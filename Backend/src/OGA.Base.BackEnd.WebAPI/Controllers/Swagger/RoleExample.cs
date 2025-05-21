using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.WebAPI.Controllers.Swagger
{
    [ExcludeFromCodeCoverage]
    public class RoleModelExample : IExamplesProvider<RoleModel>
    {
        public RoleModel GetExamples()
        {
            return new RoleModel
            {
                Name = "PLANNER",
                Description = "Planner"
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class RoleModelListExample : IExamplesProvider<IEnumerable<RoleModel>>
    {
        public IEnumerable<RoleModel> GetExamples()
        {
            return new List<RoleModel> {
                    new RoleModelExample().GetExamples()
                };
        }
    }

    [ExcludeFromCodeCoverage]
    public class RolePaginationModelExample : IExamplesProvider<DataPaginationModel<RoleModel>>
    {
        public DataPaginationModel<RoleModel> GetExamples()
        {
            return new DataPaginationModel<RoleModel>()
            {
                PageNumber = 1,
                PageSize = 10,
                TotalSize = 100,
                Data = new RoleModelListExample().GetExamples()
            };
        }
    }
}
