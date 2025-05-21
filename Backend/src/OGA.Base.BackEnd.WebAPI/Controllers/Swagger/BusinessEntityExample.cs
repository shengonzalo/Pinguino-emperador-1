using OGA.Core.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.WebAPI.Controllers.Swagger
{
    [ExcludeFromCodeCoverage]
    public class BusinessEntityModelExample : IExamplesProvider<BusinessEntityModel>
    {
        public BusinessEntityModel GetExamples()
        {
            return new BusinessEntityModel
            {
                Id = 1,
                Name = "Installation",
                Description = "Instalaciones"
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class BusinessEntityModelListExample : IExamplesProvider<IEnumerable<BusinessEntityModel>>
    {
        public IEnumerable<BusinessEntityModel> GetExamples()
        {
            return new List<BusinessEntityModel> {
                new BusinessEntityModelExample().GetExamples()
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class BusinessEntityPaginationModelExample : IExamplesProvider<DataPaginationModel<BusinessEntityModel>>
    {
        public DataPaginationModel<BusinessEntityModel> GetExamples()
        {
            return new DataPaginationModel<BusinessEntityModel>()
            {
                PageNumber = 1,
                PageSize = 10,
                TotalSize = 100,
                Data = new BusinessEntityModelListExample().GetExamples()
            };
        }
    }
}
