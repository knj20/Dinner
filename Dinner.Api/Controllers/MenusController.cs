using Dinner.Application.Services.Authentication.Common;
using Dinner.Application.Services.Menus.Commands.CreateMenu;
using Dinner.Contracts.Menus;
using Dinner.Domain.Menu;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dinner.Api.Controllers
{
    [Route("hosts/{hostId}/menus")]
    public class MenusController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public MenusController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuAsync(CreateMenuRequest request, string hostId)
        {
            var command = _mapper.Map<CreateMenuCommand>((request, hostId));

            ErrorOr<Menu> createdMenuResult =await _mediator.Send(command);

            return createdMenuResult.Match(
            menu => Ok(_mapper.Map<MenuResponse>(menu)),
            errors => Problem(errors));
        }
    }
}
