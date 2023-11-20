using Dinner.Application.Services.Authentication.Commands.Register;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinner.Application.Services.Menus.Commands.CreateMenu
{
    public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
