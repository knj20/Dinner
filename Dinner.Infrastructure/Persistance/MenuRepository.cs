using Dinner.Application.Common.Interfaces.Persistance;
using Dinner.Domain.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinner.Infrastructure.Persistance
{
    public class MenuRepository : IMenuRepository
    {
        private static readonly List<Menu> menus = new();
        public void Add(Menu menu)
        {
            menus.Add(menu);
        }
    }
}
