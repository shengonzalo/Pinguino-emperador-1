using AutoMapper;
using OGA.AAS.BackEnd.Application.Mappings.Resolvers;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.AAS.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRole))
                .ForMember(dest => dest.LanguageCode, opt => opt.MapFrom(src => src.Language!.Code))
                .ForMember(dest => dest.LanguageDesc, opt => opt.MapFrom(src => src.Language!.Description))
                .ForMember(dest => dest.IdentifierTypeDesc, opt => opt.MapFrom(src => src.IdentifierType!.Description))
                .ReverseMap()
                .ForMember(dest => dest.IdentifierType, opt => opt.Ignore())
                .ForMember(dest => dest.UserRole, opt => opt.Ignore())
                .ForMember(dest => dest.Language, opt => opt.Ignore());

            CreateMap<UserRole, RoleModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role!.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role!.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Role!.Description))
                .ReverseMap();

            CreateMap<Token, TokenModel>()
                .ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom<DateTimeResolver, DateTime>(src => src.ExpiredDate))
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore()).ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom<DateTimeResolver, string>(src => src.ExpiredDate!));

            CreateMap<TokenConfig, TokenConfigModel>()
                .ReverseMap();

            CreateMap<Role, RoleModel>()
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order == 0 ? 99 : src.Order))
                .ReverseMap()
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order ?? 99));

            CreateMap<Menu, MenuModel>()
                .ReverseMap();

            CreateMap<Resource, ResourceModel>()
               .ReverseMap();

            CreateMap<Permission, PermissionModel>()
                 .ForMember(dest => dest.ResourceName, opt => opt.MapFrom(src => src.Resource!.Name))
               .ReverseMap()
               .ForMember(dest => dest.Resource, opt => opt.Ignore());

            CreateMap<PermissionType, PermissionTypeModel>()
               .ReverseMap();
            
            CreateMap<Language, LanguageModel>()
                .ReverseMap();

            CreateMap<IdentifierType, IdentifierTypeModel>()
                .ReverseMap();

            CreateMap<Group, GroupModel>()
                .ForMember(dest => dest.RolePermissionGroup, opt => opt.MapFrom(src => src.RolePermissionGroup))
                .ReverseMap();

            CreateMap<RolePermissionGroup, RolePermissionGroupModel>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role!.Name))
                .ForMember(dest => dest.RoleDesc, opt => opt.MapFrom(src => src.Role!.Description))
                .ReverseMap()
                .ForMember(dest => dest.Group, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<UserKey, UserKeyModel>()
              .ReverseMap();

            CreateMap<User2FACode, User2FACodeModel>()
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom<DateTimeResolver, DateTime>(src => src.EndDate))
                .ReverseMap()
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom<DateTimeResolver, string>(src => src.EndDate!));
        }
    }
}
