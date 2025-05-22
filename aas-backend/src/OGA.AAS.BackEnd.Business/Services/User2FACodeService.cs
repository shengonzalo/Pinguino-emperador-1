using MediatR;
using OGA.AAS.BackEnd.Application.Registration;
using OGA.AAS.BackEnd.Business.Resources;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Custom;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Business.Services;
using OGA.Core.BackEnd.Domain.Contracts.Services;
using OGA.Core.BackEnd.Domain.Enums;
using OGA.Core.BackEnd.Domain.Exceptions;
using OGA.Core.BackEnd.Domain.Extensions;
using OGA.Core.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models.Azure;
using OGA.Core.BackEnd.Infrastructure.Azure.Contracts;
using System.Globalization;
using System.Text;

namespace OGA.AAS.BackEnd.Business.Services
{
    public class User2FACodeService : BaseAsyncService<User2FACodeModel>, IUser2FACodeService
    {
        public IBaseAsyncService<User2FACodeModel> BaseService { get; set; }

        private readonly ICommunicationService _communicationService;
        private readonly IUserService _userService;

        private readonly CurrentUserProvider _currentUserProvider;
        private readonly string _tz = DateTimeEnum.Utc;

        public User2FACodeService(IMediator mediator, ICommunicationService communicationService, 
            IUserService userService, CurrentUserProvider currentUserProvider) : base(mediator)
        {
            BaseService = this;

            _communicationService = communicationService;
            _userService = userService;
            _currentUserProvider = currentUserProvider;

            var currentUser = _currentUserProvider.GetCurrentUser();
            _tz = currentUser != null ? currentUser.TimeZoneId : _tz;
        }

        public async Task<OkResponseModel> Send2FAEmailAsync(string recipient, AuditModel audit)
        {
            var user = await _userService.GetUserByEmail(recipient) ?? throw new NotFoundException(User2FACodeMessages.UserNotFound);
            var codes = await GetUser2FACode(user.Id);

            var code = Generate2FACode();
            var duration = int.Parse(ConfigurationManager.CommunicationService2FADuration);
            var body = User2FACodeMessages.EmailCodeBody.Replace("###{0}###", user.Name).Replace("###{1}###", duration.ToString()).Replace("###{2}###", code);

            var sendResult = await _communicationService.SendEmailAsync(new EmailRequestModel()
            {
                Subject = User2FACodeMessages.EmailCodeSubject,
                HtmlContent = User2FACodeMessages.EmailTemplate.Replace("###{0}###", body),
                Recipients = new List<string>() { recipient }
            });

            if (!sendResult)
            {
                throw new BadRequestException(User2FACodeMessages.EmailError);
            }

            if (codes != null && codes.Any())
            {
                var ids = codes.Select(x => x.Id).ToList();
                await DeleteAsync(ids, audit);
            }

            var endDate = DateTime.UtcNow.AddMinutes(duration).ConvertDateTime(tzTargetId: _tz).ToString(ConfigurationManager.LongDateFormat);

            var response = await BaseService.AddAsync(new User2FACodeModel()
            {
                Id = 0,
                IdUser = user.Id,
                Code = code,
                EndDate = endDate
            }, audit);

            return response;
        }

        public async Task<bool> VerifyCode(int userId, string code)
        {
            var codes = await GetUser2FACode(userId, code);

            if (codes != null && codes.Any())
            {
                var cod = codes.FirstOrDefault();

                if (cod != null && !string.IsNullOrEmpty(cod.EndDate))
                {
                    var endDate = DateTime.ParseExact(cod.EndDate, ConfigurationManager.LongDateFormat, CultureInfo.InvariantCulture)
                        .ConvertDateTime(tzSourceId: _tz);

                    return DateTime.UtcNow <= endDate;
                }
            }

            return false;
        }

        private async Task<IEnumerable<User2FACodeModel>> GetUser2FACode(int userId, string? code = null)
        {
            var result = new List<User2FACodeModel>();

            var filter = new List<FilterModel>()
            {
                new()
                {
                    PropertyName = "IdUser",
                    SearchText = userId.ToString(),
                    Operator = OperatorEnum.Equals
                }
            };

            if (!string.IsNullOrEmpty(code))
            {
                filter.Add(new()
                {
                    PropertyName = "Code",
                    SearchText = code,
                    Operator = OperatorEnum.Equals
                });
            }

            var query = new QueryParamModel()
            {
                Pagination = new PaginationModel(),
                Order = new List<OrderModel>(),
                Filter = filter,
                ReplaceTxt = false
            };

            var codes = await BaseService.GetAllAsync(query);

            if (codes != null && codes.Data != null && codes.Data.Any())
            {
                result = codes.Data.ToList();
            }

            return result;
        }

        private async Task<IEnumerable<OkResponseModel>> DeleteAsync(IEnumerable<int> ids, AuditModel audit, bool save = true)
        {
            var result = new List<OkResponseModel>();
            var total = ids.Count();

            if (total > 0)
            {
                var index = 0;

                foreach (var id in ids)
                {
                    var last = save && (index + 1) == total;
                    result.Add(await BaseService.DeleteAsync(id, audit, save: last));
                    index++;
                }
            }

            return result;
        }

        private static string Generate2FACode()
        {
            var length = int.Parse(ConfigurationManager.CommunicationService2FALength);
            var random = new Random();
            var code = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                code.Append(random.Next(0, 10));
            }

            return code.ToString();
        }
    }
}
