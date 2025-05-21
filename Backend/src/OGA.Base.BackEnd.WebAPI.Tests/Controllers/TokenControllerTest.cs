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
    public class TokenControllerTest
    {
        private TokenController? _tokenController;
        private Mock<ITokenService>? _tokenService;

        #region Initialize&Cleanup

        [TestInitialize]
        public void Initialize()
        {
            CreateMocks();

            _tokenController = new TokenController(_tokenService!.Object)
            {
                ControllerContext = ControllerUtility.GetControllerContext()
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _tokenController = null;
        }

        #endregion Initialize&Cleanup

        #region GetToken

        [TestMethod]
        public async Task GetToken_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetTokenModel();

            _tokenService!.Setup(x => x.GetToken(It.IsAny<string[]>()))
                .ReturnsAsync(model);

            IActionResult result = await _tokenController!.GetToken();
            var data = (result as OkObjectResult)?.Value as TokenModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual("admin@admin.com", data.Email);
        }

        #endregion

        #region PostToken

        [TestMethod]
        public async Task PostToken_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetTokenModel();

            _tokenService!.Setup(x => x.PostToken(It.IsAny<string[]>(), It.IsAny<bool>()))
                .ReturnsAsync(model);

            IActionResult result = await _tokenController!.PostToken();
            var data = (result as OkObjectResult)?.Value as TokenModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual("admin@admin.com", data.Email);
        }

        #endregion PostToken

        #region IsValidToken

        [TestMethod]
        public async Task IsValidToken_OK()
        {
            var expected = typeof(OkObjectResult);

            _tokenService!.Setup(x => x.IsValidToken(It.IsAny<TokenInfoModel>()))
                .ReturnsAsync(true);

            IActionResult result = await _tokenController!.IsValidToken();
            var data = (result as OkObjectResult)?.Value as bool?;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.IsTrue(data);
        }

        #endregion IsValidToken

        #region RefreshToken

        [TestMethod]
        public async Task RefreshToken_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetTokenModel();

            _tokenService!.Setup(x => x.RefreshToken(It.IsAny<string>()))
                .ReturnsAsync(model);

            IActionResult result = await _tokenController!.RefreshToken();
            var data = (result as OkObjectResult)?.Value as TokenModel;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual("admin@admin.com", data.Email);
        }

        #endregion RefreshToken

        #region Mocks&Data

        private void CreateMocks()
        {
            _tokenService = new Mock<ITokenService>();
        }

        private static TokenModel GetTokenModel()
        {
            return new TokenModel
            {
                AccessToken = "eyfagfegrwgrggre.eydwefwegwegggdsa",
                Email = "admin@admin.com",
                ExpiredDate = "10/01/2022 14:00:00.000"
            };
        }

        #endregion Mocks&Data
    }
}
