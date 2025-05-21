using OGA.Core.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.WebAPI.Controllers.Swagger
{
    [ExcludeFromCodeCoverage]
    public class UserBusinessPermModelExample : IExamplesProvider<UserBusinessPermModel>
    {
        public UserBusinessPermModel GetExamples()
        {
            return new UserBusinessPermModel
            {
                UserId = 1,
                BusinessEntityId = 1,
                Identifier = 1
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class UserBusinessPermModelListExample : IExamplesProvider<IEnumerable<UserBusinessPermModel>>
    {
        public IEnumerable<UserBusinessPermModel> GetExamples()
        {
            return new List<UserBusinessPermModel> {
                new UserBusinessPermModelExample().GetExamples()
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class UserBusinessPermPaginationModelExample : IExamplesProvider<DataPaginationModel<UserBusinessPermModel>>
    {
        public DataPaginationModel<UserBusinessPermModel> GetExamples()
        {
            return new DataPaginationModel<UserBusinessPermModel>()
            {
                PageNumber = 1,
                PageSize = 10,
                TotalSize = 100,
                Data = new UserBusinessPermModelListExample().GetExamples()
            };
        }
    }
}
