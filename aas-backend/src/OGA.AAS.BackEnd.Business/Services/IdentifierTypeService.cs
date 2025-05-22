using MediatR;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Business.Services;

namespace OGA.AAS.BackEnd.Business.Services
{
    public class IdentifierTypeService : BaseAsyncService<IdentifierTypeModel>, IIdentifierTypeService
    {
        public IdentifierTypeService(IMediator mediator) : base(mediator)
        {
        }
    }
}
