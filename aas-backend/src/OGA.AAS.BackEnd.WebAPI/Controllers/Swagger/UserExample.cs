using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.AAS.BackEnd.WebAPI.Controllers.Swagger
{
    [ExcludeFromCodeCoverage]
    public class UserModelExample : IExamplesProvider<UserModel>
    {
        public UserModel GetExamples()
        {
            return new UserModel
            {
                Id = 1,
                IdentifierTypeId = 1,
                Identifier = "document Number",
                Name = "User Name",
                Surname = "User Surname",
                SecondSurname = "User Second Surname",
                PhoneNumber = "User Phone Number",                
                Email = "user@mail.com",
                ActiveChk = true,
                LanguageId = 1,
                Roles = new RoleModelListExample().GetExamples()
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
