using OGA.AAS.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.WebAPI.Controllers.Swagger
{
    [ExcludeFromCodeCoverage]
    public class MenuModelExample : IExamplesProvider<MenuModel>
    {
        public MenuModel GetExamples()
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
    }

    [ExcludeFromCodeCoverage]
    public class MenuModelListExample : IExamplesProvider<IEnumerable<MenuModel>>
    {
        public IEnumerable<MenuModel> GetExamples()
        {
            return new List<MenuModel> {
                    new MenuModelExample().GetExamples()
                };
        }
    }
}
