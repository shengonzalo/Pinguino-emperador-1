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
    public class LanguageControllerTest
    {
        private LanguageController? _languageController;
        private Mock<ILanguageService>? _languageService;

        #region Initialize&Cleanup

        [TestInitialize]
        public void Initialize()
        {
            CreateMocks();

            _languageController = new LanguageController(_languageService!.Object)
            {
                ControllerContext = ControllerUtility.GetControllerContext()
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _languageController = null;
        }

        #endregion Initialize&Cleanup

        #region GetAllLanguages

        [TestMethod]
        public async Task GetAllLanguages_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetDataPaginationModel();

            _languageService!.Setup(x => x.GetAllAsync(It.IsAny<QueryParamModel>()))
                .ReturnsAsync(model);

            IActionResult result = await _languageController!.GetAllLanguages(new QueryParamModel());
            var data = (result as OkObjectResult)?.Value as DataPaginationModel<LanguageModel>;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.IsNotNull(data.Data);
            Assert.AreEqual(1, data.Data.Count());
        }

        #endregion GetAllLanguages

        #region Mocks&Data

        private void CreateMocks()
        {
            _languageService = new Mock<ILanguageService>();
        }

        private static DataPaginationModel<LanguageModel> GetDataPaginationModel()
        {
            return new DataPaginationModel<LanguageModel>()
            {
                PageSize = 1,
                PageNumber = 1,
                TotalSize = 1,
                Data = new List<LanguageModel>()
                {
                    GetLanguageModel()
                }
            };
        }

        private static LanguageModel GetLanguageModel()
        {
            return new LanguageModel()
            {
                Id = 1,
                Code = "es",
                Name = "LANGUAGE",
                Description = "Language"
            };
        }

        #endregion Mocks&Data
    }
}
