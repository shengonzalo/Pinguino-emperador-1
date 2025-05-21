using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.WebAPI.Controllers.Swagger
{
    [ExcludeFromCodeCoverage]
    public class GroupModelExample : IExamplesProvider<GroupModel>
    {
        public GroupModel GetExamples()
        {
            return new GroupModel
            {
                Id = 1,
                Name = "Group Name",
                Description = "Group Description",
                RolePermissionGroup = new List<RolePermissionGroupModel>()
                {
                    new RolePermissionGroupModel()
                    {
                        GroupId = 1,
                        RoleId = 1,
                        RoleName = "PLANNER",
                        RoleDesc = "Planner"
                    }
                }
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class GroupModelListExample : IExamplesProvider<IEnumerable<GroupModel>>
    {
        public IEnumerable<GroupModel> GetExamples()
        {
            return new List<GroupModel> {
                new GroupModelExample().GetExamples()
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class GroupPaginationModelExample : IExamplesProvider<DataPaginationModel<GroupModel>>
    {
        public DataPaginationModel<GroupModel> GetExamples()
        {
            return new DataPaginationModel<GroupModel>()
            {
                PageNumber = 1,
                PageSize = 10,
                TotalSize = 100,
                Data = new GroupModelListExample().GetExamples()
            };
        }
    }
}
