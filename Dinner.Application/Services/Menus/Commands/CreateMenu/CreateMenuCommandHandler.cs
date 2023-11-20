using Dinner.Application.Common.Interfaces.Persistance;
using Dinner.Domain.Host.ValueObjects;
using Dinner.Domain.Menu;
using Dinner.Domain.Menu.Entities;
using ErrorOr;
using MediatR;

namespace Dinner.Application.Services.Menus.Commands.CreateMenu
{
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
    {
        private readonly IMenuRepository _menuRepository;

        public CreateMenuCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand command, CancellationToken cancellationToken)
        {
            // just for learning ^_^
            await Task.CompletedTask;

            // create menu
            var menu = Menu.Create(
                hostId: HostId.Create(command.HostId),
                name: command.Name,
                description: command.Description,
                sections: (List<MenuSection>)command.Sections.ConvertAll(s => MenuSection.Create(
                    s.Name,
                    s.Description,
                    items: s.Items.ConvertAll(item => MenuItem.Create(
                        item.Name,
                        item.Description
                        ))
                    ))
                );

            // persist menu

            _menuRepository.Add(menu);

            // return menu
            return menu;
        }
    }
}
