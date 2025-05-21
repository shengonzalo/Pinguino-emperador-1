using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Base.BackEnd.WebAPI.Controllers;
using OGA.Base.BackEnd.WebAPI.Tests.Utilities;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.Base.BackEnd.WebAPI.Tests.Controllers
{
    [TestClass]
    public class PermissionControllerTest
    {
        private PermissionController? _permissionController;
        private Mock<IPermissionService>? _permissionService;

        #region Initialize&Cleanup

        [TestInitialize]
        public void Initialize()
        {
            CreateMocks();

            _permissionController = new PermissionController(_permissionService!.Object)
            {
                ControllerContext = ControllerUtility.GetControllerContext()
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _permissionController = null;
        }

        #endregion Initialize&Cleanup

        #region GetUserPermissions

        [TestMethod]
        public async Task GetUserPermissions_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetUserPermissionModel();

            _permissionService!.Setup(x => x.GetUserPermissions(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(model);

            IActionResult result = await _permissionController!.GetUserPermissions(1);
            var data = (result as OkObjectResult)?.Value as UserPermissionModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.IsNotNull(data.Resource);
            Assert.AreEqual(1, data.Resource.Id);
        }

        #endregion GetUserPermissions

        #region GetAllGroups

        [TestMethod]
        public async Task GetAllGroups_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetDataPaginationModel();

            _permissionService!.Setup(x => x.GetAllGroups(It.IsAny<QueryParamModel>()))
                .ReturnsAsync(model);

            IActionResult result = await _permissionController!.GetAllGroups(new QueryParamModel());
            var data = (result as OkObjectResult)?.Value as DataPaginationModel<GroupModel>;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.IsNotNull(data.Data);
            Assert.AreEqual(1, data.Data.Count());
        }

        #endregion GetAllGroups

        #region UpdateRolePermGroup

        [TestMethod]
        public async Task UpdateRolePermGroup_OK()
        {
            var expected = typeof(OkObjectResult);
            var response = GetResponse();

            _permissionService!.Setup(x => x.UpdateRolePermGroup(It.IsAny<IEnumerable<RolePermissionGroupModel>>(), It.IsAny<AuditModel>()))
                .ReturnsAsync(new List<OkResponseModel>() { response });

            IActionResult result = await _permissionController!.UpdateRolePermGroup(new List<RolePermissionGroupModel>());
            var data = (result as OkObjectResult)?.Value as IEnumerable<OkResponseModel>;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(response.Id, data.ElementAt(0).Id);
        }

        #endregion UpdateRolePermGroup

        #region Mocks&Data

        private void CreateMocks()
        {
            _permissionService = new Mock<IPermissionService>();
        }

        private static OkResponseModel GetResponse()
        {
            return new OkResponseModel()
            {
                Id = 1,
                Message = string.Empty
            };
        }

        private static UserPermissionModel GetUserPermissionModel()
        {
            return new UserPermissionModel()
            {
                Resource = new ResourcePermissionModel()
                {
                    Id = 1,
                    Name = "Pantalla 1",
                    Description = "Descripción de la pantalla 1"
                },
                Permissions = new List<PermissionModel>()
                {
                    new PermissionModel()
                    {
                        Id = 1,
                        Name = "Permiso 1",
                        Description = "Descripción del permiso 1",
                        Allowed = true,
                        PermissionType = new PermissionTypeModel()
                        {
                            Id = 1,
                            Description = "Descripción del tipo de permiso 1"
                        }
                    }
                }
            };
        }

        private static DataPaginationModel<GroupModel> GetDataPaginationModel()
        {
            return new DataPaginationModel<GroupModel>()
            {
                PageSize = 1,
                PageNumber = 1,
                TotalSize = 1,
                Data = new List<GroupModel>()
                {
                    GetGroupModel()
                }
            };
        }

        private static GroupModel GetGroupModel()
        {
            return new GroupModel()
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

        #endregion Mocks&Data
    }
}
