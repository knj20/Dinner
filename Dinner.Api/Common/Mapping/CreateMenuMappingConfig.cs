using Dinner.Application.Services.Menus.Commands.CreateMenu;
using Dinner.Contracts.Menus;
using Dinner.Domain.Menu;
using Dinner.Domain.Menu.Entities;
using Mapster;

namespace Dinner.Api.Common.Mapping
{
    public class CreateMenuMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CreateMenuRequest request, string hostId), CreateMenuCommand>()
                .Map(dest => dest.HostId, src => src.hostId)
                .Map(dest => dest, src => src.request);

            config.NewConfig<Menu, MenuResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.AverageRating, src => src.AverageRating > 0 ? src.AverageRating : null)
                .Map(dest => dest.HostId, src => src.HostId)
                .Map(dest => dest.DinnerIds, src => src.DinnerIds.Select(id => id.Value))
                .Map(dest => dest.MenuReviewIds, src => src.MenuReviewIds.Select(id => id.Value));

            config.NewConfig<MenuSection, MenuSectionResponse>()
                .Map(dest => dest.Id, src => src.Id.Value);

            config.NewConfig<MenuItem, MenuItemResponse>()
                .Map(dest => dest.Id, src => src.Id.Value);
        }
    }
}
