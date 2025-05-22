using MediatR;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Business.Services;

namespace OGA.AAS.BackEnd.Business.Services
{
    public class LanguageService : BaseAsyncService<LanguageModel>, ILanguageService
    {
        public LanguageService(IMediator mediator) : base(mediator)
        {
        }

    }
}
