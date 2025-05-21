using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Base.BackEnd.WebAPI.Controllers;
using OGA.Base.BackEnd.WebAPI.Tests.Utilities;

namespace OGA.Base.BackEnd.WebAPI.Tests.Controllers
{
    [TestClass]
    public class MenuControllerTest
    {
        private MenuController? _menuController;
        private Mock<IMenuService>? _menuService;

        #region Initialize&Cleanup

        [TestInitialize]
        public void Initialize()
        {
            CreateMocks();

            _menuController = new MenuController(_menuService!.Object)
            {
                ControllerContext = ControllerUtility.GetControllerContext()
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _menuController = null;
        }

        #endregion Initialize&Cleanup

        #region GetMenu

        [TestMethod]
        public async Task GetMenu_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = new List<MenuModel>()
            {
                GetMenuModel()
            };

            _menuService!.Setup(x => x.GetMenu())
                .ReturnsAsync((IEnumerable<MenuModel>)model);

            IActionResult result = await _menuController!.GetMenu();
            var data = (result as OkObjectResult)?.Value as IEnumerable<MenuModel>;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Count());
        }

        #endregion GetMenu

        #region Mocks&Data

        private void CreateMocks()
        {
            _menuService = new Mock<IMenuService>();
        }

        private static MenuModel GetMenuModel()
        {
            return new MenuModel()
            {
                Id = 1,
                Name = "Menu 1",
                Description = "Menú padre 1",
                RootId = 1,
                MenuItems = new List<MenuModel>()
                {
                    new MenuModel()
                    {
                        Id = 2,
                        Name = "Submenu 1.1",
                        Description = "Menú hijo 1.1",
                        RootId = 1,
                        ParentId = 1
                    }
                }
            };
        }

        #endregion Mocks&Data
    }
}
