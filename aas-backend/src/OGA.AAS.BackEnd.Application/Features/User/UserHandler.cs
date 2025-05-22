using AutoMapper;
using MediatR;
using OGA.AAS.BackEnd.Application.Features.User.Commands;
using OGA.AAS.BackEnd.Application.Features.User.Queries;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Application.Messages;
using OGA.Core.BackEnd.Domain.Exceptions;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Features.User
{
    public class UserHandler :
        IRequestHandler<GetAllUsersQuery, IEnumerable<UserModel>>,
        IRequestHandler<GetUserByEmailQuery, UserModel>,
        IRequestHandler<GetUserByRolQuery, IEnumerable<UserModel>>,
        IRequestHandler<CreateUserCommand, OkResponseModel>,
        IRequestHandler<UpdateUserCommand, OkResponseModel>,
        IRequestHandler<DeleteUserCommand, OkResponseModel>,
        IRequestHandler<ChangeUserPasswordCommand, OkResponseModel>,
        IRequestHandler<GetUserKeysByUserIdQuery, IEnumerable<UserKeyModel>>,
        IRequestHandler<GetThreeLastsUserKeysByUserIdQuery, IEnumerable<UserKeyModel>>,
        IRequestHandler<GetLastUserKeyByUserIdQuery, UserKeyModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserKeyRepository _userKeyRepository;
        private readonly IMapper _mapper;

        public UserHandler(IUserRepository userRepository, IMapper mapper, IUserKeyRepository userKeyRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userKeyRepository = userKeyRepository;
        }

        public async Task<IEnumerable<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsers(request.Pagination, request.Order, request.Filter);
            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<IEnumerable<UserModel>> Handle(GetUserByRolQuery request, CancellationToken cancellationToken)
        {
            var listUser = await _userRepository.GetUserByRol(request.IdRol);
            return _mapper.Map<IEnumerable<UserModel>>(listUser);
        }

        public async Task<OkResponseModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {           
            var entity = _mapper.Map<Domain.Entities.User>(request.Model);
            entity.IDate = request.Audit.IDate;
            entity.IUser = request.Audit.IUser;
            entity.IComments = request.Audit.IComments;
            entity.Enabled = true;

            if (request.Model.Roles != null && request.Model.Roles.Any())
            {
                var roles = new List<UserRole>();

                foreach (var rol in request.Model.Roles)
                {
                    roles.Add(new UserRole()
                    {
                        User = entity,
                        RoleId = rol.Id,
                        IDate = request.Audit.IDate,
                        IUser = request.Audit.IUser,
                        IComments = request.Audit.IComments,
                        Enabled = true
                    });
                }

                entity.UserRole = roles;
            }
            var userKey = new List<UserKey>
            {
                new UserKey
                {
                    CreatedDate = DateTime.UtcNow,
                    Enabled = true,
                    IDate = request.Audit.IDate,
                    IUser = request.Audit.IUser,
                    IComments = request.Audit.IComments,
                    PasswordHash = entity.PasswordHash
                }
             };
            entity.UserKeys = userKey;

            return await _userRepository.AddAsync(entity, request.Save);
        }

        public async Task<OkResponseModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.GetByIdAsync(request.Id);
            var roles = new List<UserRole>();

            if (entity == null)
            {
                throw new NotFoundException(ValidatorMessages.NullEntity);
            }

            request.Model.PasswordHash = entity.PasswordHash;

            _mapper.Map(request.Model, entity);

            entity.UDate = request.Audit.UDate;
            entity.UUser = request.Audit.UUser;
            entity.UComments = request.Audit.UComments;

            if (request.Model.Roles != null && request.Model.Roles.Any())
            {
                foreach (var rol in request.Model.Roles)
                {
                    roles.Add(new UserRole()
                    {
                        RoleId = rol.Id
                    });
                }
            }

            return await _userRepository.UpdateUser(entity, roles);
        }

        public async Task<OkResponseModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(ValidatorMessages.NullEntity);

            entity.UDate = request.Audit.UDate;
            entity.UUser = request.Audit.UUser;
            entity.UComments = request.Audit.UComments;
            entity.Enabled = false;

            var userkies = await _userKeyRepository.GetAllByUserIdAsync(request.Id);

            if(userkies != null && userkies.Any())
            {
                foreach(var userkey in userkies)
                {
                    if (userkey == null)
                        continue;

                    userkey.UDate = request.Audit.UDate;
                    userkey.UUser = request.Audit.UUser;
                    userkey.UComments = request.Audit.UComments;
                    userkey.Enabled = false;
                }
            }

            return await _userRepository.DeleteUser(entity);
        }

        public async Task<OkResponseModel> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(ValidatorMessages.NullEntity);

            entity.UDate = request.Audit.UDate;
            entity.UUser = request.Audit.UUser;
            entity.UComments = request.Audit.UComments;
            entity.PasswordHash = request.newPassword;

            await CalculateUserKeyAsync(entity, request.Audit);
            
            return await _userRepository.UpdateAsync(entity);
        }
        public async Task<IEnumerable<UserKeyModel>> Handle(GetUserKeysByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userKeys = await _userKeyRepository.GetAllByUserIdAsync(request.IdUser);
            return _mapper.Map<IEnumerable<UserKeyModel>>(userKeys);
        }
        public async Task<UserKeyModel> Handle(GetLastUserKeyByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userKey = await _userKeyRepository.GetLastByUserIdAsync(request.IdUser);
            return _mapper.Map<UserKeyModel>(userKey);
        }
        public async Task<IEnumerable<UserKeyModel>> Handle(GetThreeLastsUserKeysByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userKeys = await _userKeyRepository.GetLastThreeByUserIdAsync(request.IdUser, request.LastKeys);
            return _mapper.Map<IEnumerable<UserKeyModel>>(userKeys);
        }

        private async Task CalculateUserKeyAsync(Domain.Entities.User user, AuditModel audit)
        {

            var entity = await _userKeyRepository.GetLastByUserIdAsync(user.Id);

            if(entity == null || entity.CreatedDate.AddDays(Registration.ConfigurationManager.ConfigAuditUserKey.ExpiredKeyInDays) < DateTime.UtcNow)
            {
                var userkey = new UserKey
                {
                    CreatedDate = DateTime.UtcNow,
                    Enabled = true,
                    IDate = DateTime.UtcNow,
                    IUser = Registration.ConfigurationManager.WebAPIName,
                    IComments = audit.UComments,
                    PasswordHash = user.PasswordHash,
                    IdUser = user.Id
                };
                await _userKeyRepository.AddAsync(userkey, false);
            }
            else
            {
                entity.UUser = Registration.ConfigurationManager.WebAPIName;
                entity.UDate = audit.UDate;
                entity.UComments = audit.UComments;
                entity.PasswordHash = user.PasswordHash;
                entity.CreatedDate = DateTime.UtcNow;

                await _userKeyRepository.UpdateAsync(entity, false);    
            }
        }

        
    }
}
