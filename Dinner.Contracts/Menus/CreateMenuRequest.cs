using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinner.Contracts.Menus
{
    public record CreateMenuRequest
    (   string Name,
        string Description,
        List<MenuSectionRequest> Sections
    );

    public record MenuSectionRequest
    (
        string Name,
        string Description,
        List<MenuItemRequest> Items
    );

    public record MenuItemRequest
    (
        string Name,
        string Description
    );
}
