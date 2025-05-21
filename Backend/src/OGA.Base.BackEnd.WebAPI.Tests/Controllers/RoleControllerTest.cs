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
    public class RoleControllerTest
    {
        private RoleController? _roleController;
        private Mock<IRoleService>? _roleService;

        #region Initialize&Cleanup

        [TestInitialize]
        public void Initialize()
        {
            CreateMocks();

            _roleController = new RoleController(_roleService!.Object)
            {
                ControllerContext = ControllerUtility.GetControllerContext()
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _roleController = null;
        }

        #endregion Initialize&Cleanup

        #region GetAllRoles

        [TestMethod]
        public async Task GetAllRoles_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetDataPaginationModel();

            _roleService!.Setup(x => x.GetAllAsync(It.IsAny<QueryParamModel>()))
                .ReturnsAsync(model);

            IActionResult result = await _roleController!.GetAllRoles(new QueryParamModel());
            var data = (result as OkObjectResult)?.Value as DataPaginationModel<RoleModel>;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.IsNotNull(data.Data);
            Assert.AreEqual(1, data.Data.Count());
        }

        #endregion GetAllRoles

        #region GetRoleById

        [TestMethod]
        public async Task GetRoleById_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetRoleModel();

            _roleService!.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<BusinessPermModel>(), It.IsAny<bool>()))
                .ReturnsAsync((RoleModel?)model);

            IActionResult result = await _roleController!.GetRoleById(1);
            var data = (result as OkObjectResult)?.Value as RoleModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Id);
        }

        #endregion

        #region CreateRole

        [TestMethod]
        public async Task CreateRole_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetRoleModel();
            var response = GetResponse();

            _roleService!.Setup(x => x.AddAsync(It.IsAny<RoleModel>(), It.IsAny<AuditModel>(), It.IsAny<BusinessPermModel>(), It.IsAny<bool>()))
                .ReturnsAsync(response);

            IActionResult result = await _roleController!.CreateRole(model);
            var data = (result as OkObjectResult)?.Value as OkResponseModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(response.Id, data.Id);
        }

        #endregion CreateRole

        #region UpdateRole

        [TestMethod]
        public async Task UpdateRole_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetRoleModel();
            var response = GetResponse();

            _roleService!.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<RoleModel>(), It.IsAny<AuditModel>(), It.IsAny<BusinessPermModel>(), It.IsAny<bool>()))
                .ReturnsAsync(response);

            IActionResult result = await _roleController!.UpdateRole(1, model);
            var data = (result as OkObjectResult)?.Value as OkResponseModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(response.Id, data.Id);
        }

        #endregion UpdateRole

        #region DeleteRole

        [TestMethod]
        public async Task DeleteRole_OK()
        {
            var expected = typeof(OkObjectResult);
            var response = GetResponse();

            _roleService!.Setup(x => x.DeleteRole(It.IsAny<int>(), It.IsAny<AuditModel>()))
                .ReturnsAsync(response);

            IActionResult result = await _roleController!.DeleteRole(1);
            var data = (result as OkObjectResult)?.Value as OkResponseModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(response.Id, data.Id);
        }

        #endregion DeleteRole

        #region Mocks&Data

        private void CreateMocks()
        {
            _roleService = new Mock<IRoleService>();
        }

        private static OkResponseModel GetResponse()
        {
            return new OkResponseModel()
            {
                Id = 1,
                Message = string.Empty
            };
        }

        private static DataPaginationModel<RoleModel> GetDataPaginationModel()
        {
            return new DataPaginationModel<RoleModel>()
            {
                PageSize = 1,
                PageNumber = 1,
                TotalSize = 1,
                Data = new List<RoleModel>()
                {
                    GetRoleModel()
                }
            };
        }

        private static RoleModel GetRoleModel()
        {
            return new RoleModel()
            {
                Id = 1,
                Name = "ROLE",
                Description = "Role"
            };
        }

        #endregion Mocks&Data
    }
}
