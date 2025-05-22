using MediatR;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Business.Services;
using OGA.Core.BackEnd.Domain.Models;

namespace OGA.AAS.BackEnd.Business.Services
{
    public class MenuService : BaseAsyncService<MenuModel>, IMenuService
    {
        public MenuService(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<MenuModel>> GetMenu()
        {
            var data = await GetAllAsync(new QueryParamModel()); 
            var model = new List<MenuModel>();

            if (data != null && data.Data != null) 
            {
                var menu = data.Data.OrderBy(x => x.Order);
                foreach (var m in menu.Where(x => !x.ParentId.HasValue))
                {
                    var menuItems = menu.Where(x => x.Id != m.Id && x.RootId == m.Id);
                    m.MenuItems = GetMenuRecursive(m.Id, menuItems);
                    model.Add(m);
                }
            }

            return model;
        }

        private IEnumerable<MenuModel> GetMenuRecursive(int parentId, IEnumerable<MenuModel> menuItems)
        {
            var model = new List<MenuModel>();
            
            foreach (var m in menuItems.Where(x => x.ParentId == parentId))
            {
                m.MenuItems = GetMenuRecursive(m.Id, menuItems);
                model.Add(m);
            }

            return model;
        }
    }
}
