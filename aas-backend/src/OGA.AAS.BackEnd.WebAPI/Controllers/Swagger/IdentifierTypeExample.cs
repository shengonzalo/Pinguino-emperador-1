using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.AAS.BackEnd.WebAPI.Controllers.Swagger
{
    public class IdentifierTypeModelExample : IExamplesProvider<IdentifierTypeModel>
    {
        public IdentifierTypeModel GetExamples()
        {
            return new IdentifierTypeModel
            {
                Id = 1,
                Description = "IdentifierType"
            };
        }

    }
    [ExcludeFromCodeCoverage]
    public class IdentifierTypeModelListExample : IExamplesProvider<IEnumerable<IdentifierTypeModel>>
    {
        public IEnumerable<IdentifierTypeModel> GetExamples()
        {
            return new List<IdentifierTypeModel> {
                new IdentifierTypeModelExample().GetExamples()
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class IdentifierTypePaginationModelExample : IExamplesProvider<DataPaginationModel<IdentifierTypeModel>>
    {
        public DataPaginationModel<IdentifierTypeModel> GetExamples()
        {
            return new DataPaginationModel<IdentifierTypeModel>()
            {
                PageNumber = 1,
                PageSize = 10,
                TotalSize = 100,
                Data = new IdentifierTypeModelListExample().GetExamples()
            };
        }
    }
}
