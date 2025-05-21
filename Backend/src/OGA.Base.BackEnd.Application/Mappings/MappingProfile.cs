using AutoMapper;
using OGA.Base.BackEnd.Domain.Entities;
using OGA.Base.BackEnd.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.Application.Mappings
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActionsAudit, ActionsAuditModel>()
                .ReverseMap();
        }
    }
}
