using Dinner.Domain.Menu;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinner.Application.Services.Menus.Commands.CreateMenu
{
    public record CreateMenuCommand
    (
        string Name,
        string Description,
        string HostId,
        List<MenuSectionCommand> Sections
    ) : IRequest<ErrorOr<Menu>>;

    public record MenuSectionCommand
    (
        string Name,
        string Description,
        List<MenuItemCommand> Items
    );

    public record MenuItemCommand
    (
        string Name,
        string Description
    );
}
