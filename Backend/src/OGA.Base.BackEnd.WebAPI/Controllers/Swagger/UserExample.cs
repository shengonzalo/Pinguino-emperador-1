using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.WebAPI.Controllers.Swagger
{
    [ExcludeFromCodeCoverage]
    public class UserModelExample : IExamplesProvider<UserModel>
    {
        public UserModel GetExamples()
        {
            return new UserModel
            {
                Id = 1,
                Email = "user@mail.com"
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class UserModelListExample : IExamplesProvider<IEnumerable<UserModel>>
    {
        public IEnumerable<UserModel> GetExamples()
        {
            return new List<UserModel> {
                new UserModelExample().GetExamples()
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class UserPaginationModelExample : IExamplesProvider<DataPaginationModel<UserModel>>
    {
        public DataPaginationModel<UserModel> GetExamples()
        {
            return new DataPaginationModel<UserModel>()
            {
                PageNumber = 1,
                PageSize = 10,
                TotalSize = 100,
                Data = new UserModelListExample().GetExamples()
            };
        }
    }
}
