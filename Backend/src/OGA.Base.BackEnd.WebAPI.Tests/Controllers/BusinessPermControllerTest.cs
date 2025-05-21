using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OGA.Base.BackEnd.WebAPI.Controllers;
using OGA.Base.BackEnd.WebAPI.Tests.Utilities;
using OGA.Core.BackEnd.Domain.Contracts.Services;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.Base.BackEnd.WebAPI.Tests.Controllers
{
    [TestClass]
    public class BusinessPermControllerTest
    {
        private BusinessPermController? _businessPermController;
        private Mock<IBusinessEntityService>? _businessEntityService;
        private Mock<IUserBusinessPermService>? _userBusinessPermService;

        #region Initialize&Cleanup

        [TestInitialize]
        public void Initialize()
        {
            CreateMocks();

            _businessPermController = new BusinessPermController(_businessEntityService!.Object, _userBusinessPermService!.Object)
            {
                ControllerContext = ControllerUtility.GetControllerContext()
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _businessPermController = null;
        }

        #endregion Initialize&Cleanup

        #region GetAllBusinessEntities

        [TestMethod]
        public async Task GetAllBusinessEntities_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetBusinessEntityPaginationModel();

            _businessEntityService!.Setup(x => x.GetAllBusinessEntities(It.IsAny<QueryParamModel>()))
                .ReturnsAsync(model);

            IActionResult result = await _businessPermController!.GetAllBusinessEntities(new QueryParamModel());
            var data = (result as OkObjectResult)?.Value as DataPaginationModel<BusinessEntityModel>;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.IsNotNull(data.Data);
            Assert.AreEqual(1, data.Data.Count());
        }

        #endregion GetAllBusinessEntities

        #region GetAllUserBusinessPerms

        [TestMethod]
        public async Task GetAllUserBusinessPerms_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetUserBusinessPermPaginationModel();

            _userBusinessPermService!.Setup(x => x.GetAllUserBusinessPerms(It.IsAny<QueryParamModel>()))
                .ReturnsAsync(model);

            IActionResult result = await _businessPermController!.GetAllUserBusinessPerms(new QueryParamModel());
            var data = (result as OkObjectResult)?.Value as DataPaginationModel<UserBusinessPermModel>;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.IsNotNull(data.Data);
            Assert.AreEqual(1, data.Data.Count());
        }

        #endregion GetAllUserBusinessPerms

        #region CreateUserBusinessPerm

        [TestMethod]
        public async Task CreateInstallation_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetUserBusinessPermModel();
            var response = GetResponse();

            _userBusinessPermService!.Setup(x => x.AddUserBusinessPerm(It.IsAny<UserBusinessPermModel>(), It.IsAny<AuditModel>()))
                .ReturnsAsync(response);

            IActionResult result = await _businessPermController!.CreateUserBusinessPerm(model);
            var data = (result as OkObjectResult)?.Value as OkResponseModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(response.Id, data.Id);
        }

        #endregion CreateUserBusinessPerm

        #region DeleteUserBusinessPerm

        [TestMethod]
        public async Task DeleteUserBusinessPerm_OK()
        {
            var expected = typeof(OkObjectResult);
            var response = GetResponse();

            _userBusinessPermService!.Setup(x => x.DeleteUserBusinessPerm(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<AuditModel>()))
                .ReturnsAsync(response);

            IActionResult result = await _businessPermController!.DeleteUserBusinessPerm(1, 1, 1);
            var data = (result as OkObjectResult)?.Value as OkResponseModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(response.Id, data.Id);
        }

        #endregion DeleteUserBusinessPerm

        #region Mocks&Data

        private void CreateMocks()
        {
            _businessEntityService = new Mock<IBusinessEntityService>();
            _userBusinessPermService = new Mock<IUserBusinessPermService>();
        }

        private static OkResponseModel GetResponse()
        {
            return new OkResponseModel()
            {
                Id = 1,
                Message = string.Empty
            };
        }

        private static DataPaginationModel<BusinessEntityModel> GetBusinessEntityPaginationModel()
        {
            return new DataPaginationModel<BusinessEntityModel>()
            {
                PageSize = 1,
                PageNumber = 1,
                TotalSize = 1,
                Data = new List<BusinessEntityModel>()
                {
                    GetBusinessEntityModel()
                }
            };
        }

        private static BusinessEntityModel GetBusinessEntityModel()
        {
            return new BusinessEntityModel()
            {
                Id = 1,
                Name = "Installation",
                Description = "Instalaciones"
            };
        }

        private static DataPaginationModel<UserBusinessPermModel> GetUserBusinessPermPaginationModel()
        {
            return new DataPaginationModel<UserBusinessPermModel>()
            {
                PageSize = 1,
                PageNumber = 1,
                TotalSize = 1,
                Data = new List<UserBusinessPermModel>()
                {
                    GetUserBusinessPermModel()
                }
            };
        }

        private static UserBusinessPermModel GetUserBusinessPermModel()
        {
            return new UserBusinessPermModel()
            {
                UserId = 1,
                BusinessEntityId = 1,
                Identifier = 1
            };
        }

        #endregion Mocks&Data
    }
}
