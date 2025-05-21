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
    public class UserControllerTest
    {
        private UserController? _userController;
        private Mock<IUserService>? _userService;

        #region Initialize&Cleanup

        [TestInitialize]
        public void Initialize()
        {
            CreateMocks();

            _userController = new UserController(_userService!.Object)
            {
                ControllerContext = ControllerUtility.GetControllerContext()
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _userController = null;
        }

        #endregion Initialize&Cleanup

        #region GetAllUsers

        [TestMethod]
        public async Task GetAllUsers_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetDataPaginationModel();

            _userService!.Setup(x => x.GetAllUsers(It.IsAny<QueryParamModel>()))
                .ReturnsAsync(model);

            IActionResult result = await _userController!.GetAllUsers(new QueryParamModel());
            var data = (result as OkObjectResult)?.Value as DataPaginationModel<UserModel>;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.IsNotNull(data.Data);
            Assert.AreEqual(1, data.Data.Count());
        }

        #endregion GetAllUsers

        #region GetUserById

        [TestMethod]
        public async Task GetUserById_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetUserModel();

            _userService!.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<BusinessPermModel>(), It.IsAny<bool>()))
                .ReturnsAsync((UserModel?)model);

            IActionResult result = await _userController!.GetUserById(1);
            var data = (result as OkObjectResult)?.Value as UserModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Id);
        }

        #endregion

        #region CreateUser

        [TestMethod]
        public async Task CreateUser_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetUserModel();
            var response = GetResponse();

            _userService!.Setup(x => x.CreateUser(It.IsAny<UserModel>(), It.IsAny<AuditModel>()))
                .ReturnsAsync(response);

            IActionResult result = await _userController!.CreateUser(model);
            var data = (result as OkObjectResult)?.Value as OkResponseModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(response.Id, data.Id);
        }

        #endregion CreateUser

        #region UpdateUser

        [TestMethod]
        public async Task UpdateUser_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetUserModel();
            var response = GetResponse();

            _userService!.Setup(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<UserModel>(), It.IsAny<AuditModel>()))
                .ReturnsAsync(response);

            IActionResult result = await _userController!.UpdateUser(1, model);
            var data = (result as OkObjectResult)?.Value as OkResponseModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(response.Id, data.Id);
        }

        #endregion UpdateUser

        #region DeleteUser

        [TestMethod]
        public async Task DeleteUser_OK()
        {
            var expected = typeof(OkObjectResult);
            var response = GetResponse();

            _userService!.Setup(x => x.DeleteUser(It.IsAny<int>(), It.IsAny<AuditModel>()))
                .ReturnsAsync(response);

            IActionResult result = await _userController!.DeleteUser(1);
            var data = (result as OkObjectResult)?.Value as OkResponseModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(response.Id, data.Id);
        }

        #endregion DeleteUser

        #region Mocks&Data

        private void CreateMocks()
        {
            _userService = new Mock<IUserService>();
        }

        private static OkResponseModel GetResponse()
        {
            return new OkResponseModel()
            {
                Id = 1,
                Message = string.Empty
            };
        }

        private static DataPaginationModel<UserModel> GetDataPaginationModel()
        {
            return new DataPaginationModel<UserModel>()
            {
                PageSize = 1,
                PageNumber = 1,
                TotalSize = 1,
                Data = new List<UserModel>()
                {
                    GetUserModel()
                }
            };
        }

        private static UserModel GetUserModel()
        {
            return new UserModel()
            {
                Id = 1,
                Email = "user@mail.com"
            };
        }

        #endregion Mocks&Data
    }
}
