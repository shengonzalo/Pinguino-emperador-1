using AutoMapper;
using OGA.AAS.BackEnd.Application.Registration;
using OGA.AAS.BackEnd.Domain.Custom;
using OGA.Core.BackEnd.Application.Mappings.Extensions;
using OGA.Core.BackEnd.Domain.Commons;
using OGA.Core.BackEnd.Domain.Enums;

namespace OGA.AAS.BackEnd.Application.Mappings.Resolvers
{
    public class DateResolver :
        IMemberValueResolver<BaseModel, BaseEntity, string, DateTime>,
        IMemberValueResolver<BaseModel, BaseEntity, string?, DateTime?>,
        IMemberValueResolver<BaseEntity, BaseModel, DateTime, string?>,
        IMemberValueResolver<BaseEntity, BaseModel, DateTime?, string?>
    {
        protected readonly CurrentUserProvider _currentUserProvider;
        protected readonly string _tz = DateTimeEnum.Utc;

        public DateResolver(CurrentUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;

            var currentUser = _currentUserProvider.GetCurrentUser();
            _tz = currentUser != null ? currentUser.TimeZoneId : _tz;
        }

        public virtual DateTime Resolve(BaseModel source, BaseEntity destination, string sourceMember, DateTime destMember, ResolutionContext context)
        {
            var aux = sourceMember.StringToDate(ConfigurationManager.ShortDateFormat, _tz, DateTimeEnum.Utc);
            var diff = Core.BackEnd.Domain.Extensions.DateTimeExtension.GetHoursDiff(_tz);
            return aux.AddHours(diff).Date;
        }

        public virtual DateTime? Resolve(BaseModel source, BaseEntity destination, string? sourceMember, DateTime? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                var aux = sourceMember.StringToDate(ConfigurationManager.ShortDateFormat, _tz, DateTimeEnum.Utc);
                var diff = Core.BackEnd.Domain.Extensions.DateTimeExtension.GetHoursDiff(_tz);
                return aux.AddHours(diff).Date;
            }
            else
            {
                return null;
            }
        }

        public virtual string? Resolve(BaseEntity source, BaseModel destination, DateTime sourceMember, string? destMember, ResolutionContext context)
        {
            return sourceMember.DateToString(ConfigurationManager.ShortDateFormat, DateTimeEnum.Utc, _tz);
        }

        public virtual string? Resolve(BaseEntity source, BaseModel destination, DateTime? sourceMember, string? destMember, ResolutionContext context)
        {
            if (sourceMember.HasValue)
            {
                return sourceMember.Value.DateToString(ConfigurationManager.ShortDateFormat, DateTimeEnum.Utc, _tz);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
