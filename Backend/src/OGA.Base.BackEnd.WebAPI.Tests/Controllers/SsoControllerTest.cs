using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Base.BackEnd.Domain.Contracts.Services;
using OGA.Base.BackEnd.Domain.Models;
using OGA.Base.BackEnd.Domain.Resource.Sso;
using OGA.Base.BackEnd.WebAPI.Controllers;
using OGA.Base.BackEnd.WebAPI.Tests.Utilities;

namespace Samu.Scheduler.BackEnd.WebAPI.Tests.Controllers;

[TestClass]
public class SsoControllerTest
{
    private SsoController? _ssoController;
    private Mock<ISsoAuthService>? _ssoAuthService;

    #region Initialize&Cleanup

    [TestInitialize]
    public void Initialize()
    {
        CreateMocks();

        _ssoController = new SsoController(_ssoAuthService!.Object)
        {
            ControllerContext = ControllerUtility.GetControllerContext()
        };
    }

    [TestCleanup]
    public void Cleanup()
    {
        _ssoController = null;
    }

    #endregion Initialize&Cleanup

    #region SignInWithMicrosoft

    [TestMethod]
    public async Task SignInWithMicrosoft_OK()
    {
        var expected = typeof(OkObjectResult);

        _ssoAuthService!.Setup(x => x.AuthenticateWithMicrosoft(It.IsAny<SsoModel>()))
           .ReturnsAsync(new TokenModel() { Email = "admin@company.com" });

        IActionResult result = await _ssoController!.SignInWithMicrosoft(new() { Token = "token" });
        var data = (result as OkObjectResult)?.Value as TokenModel;

        Assert.IsInstanceOfType(result, expected);
        Assert.IsNotNull(data);
        Assert.AreEqual("admin@company.com", data.Email);
    }

    [TestMethod]
    public async Task SignInWithMicrosoft_BadRequest()
    {
        var expected = typeof(BadRequestObjectResult);

        IActionResult result = await _ssoController!.SignInWithMicrosoft(new());
        var data = (result as BadRequestObjectResult)?.Value;

        Assert.IsInstanceOfType(result, expected);
        Assert.AreEqual(SsoResources.TOKENMICROSOFTREQUIERED, data);
    }

    [TestMethod]
    public async Task SignInWithMicrosoft_Unauthorized()
    {
        var expected = typeof(UnauthorizedObjectResult);

        _ssoAuthService!.Setup(x => x.AuthenticateWithMicrosoft(It.IsAny<SsoModel>()))
           .ReturnsAsync(() => null);

        IActionResult result = await _ssoController!.SignInWithMicrosoft(new() { Token = "token" });
        var data = (result as UnauthorizedObjectResult)?.Value;

        Assert.IsInstanceOfType(result, expected);
        Assert.AreEqual(SsoResources.AUTHENTICATEFAIL, data);
    }

    #endregion SignInWithMicrosoft

    #region Mocks&Data

    private void CreateMocks()
    {
        _ssoAuthService = new Mock<ISsoAuthService>();
    }

    #endregion Mocks&Data
}
