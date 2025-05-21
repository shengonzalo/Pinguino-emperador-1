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
    public class IdentifierTypeControllerTest
    {
        private IdentifierTypeController? _identifierTypeController;
        private Mock<IIdentifierTypeService>? _identifierTypeService;

        #region Initialize&Cleanup

        [TestInitialize]
        public void Initialize()
        {
            CreateMocks();

            _identifierTypeController = new IdentifierTypeController(_identifierTypeService!.Object)
            {
                ControllerContext = ControllerUtility.GetControllerContext()
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _identifierTypeController = null;
        }

        #endregion Initialize&Cleanup

        #region GetAllIdentifierTypes

        [TestMethod]
        public async Task GetAllIdentifierTypes_OK()
        {
            var expected = typeof(OkObjectResult);
            var model = GetDataPaginationModel();

            _identifierTypeService!.Setup(x => x.GetAllAsync(It.IsAny<QueryParamModel>()))
                .ReturnsAsync(model);

            IActionResult result = await _identifierTypeController!.GetAllIdentifierTypes(new QueryParamModel());
            var data = (result as OkObjectResult)?.Value as DataPaginationModel<IdentifierTypeModel>;

            Assert.IsInstanceOfType(result, expected);
            Assert.IsNotNull(data);
            Assert.IsNotNull(data.Data);
            Assert.AreEqual(1, data.Data.Count());
        }

        #endregion GetAllIdentifierTypes

        #region Mocks&Data

        private void CreateMocks()
        {
            _identifierTypeService = new Mock<IIdentifierTypeService>();
        }

        private static DataPaginationModel<IdentifierTypeModel> GetDataPaginationModel()
        {
            return new DataPaginationModel<IdentifierTypeModel>()
            {
                PageSize = 1,
                PageNumber = 1,
                TotalSize = 1,
                Data = new List<IdentifierTypeModel>()
                {
                    GetIdentifierTypeModel()
                }
            };
        }

        private static IdentifierTypeModel GetIdentifierTypeModel()
        {
            return new IdentifierTypeModel()
            {
                Id = 1,
                Description = "IdentifierType"
            };
        }

        #endregion Mocks&Data
    }
}
