﻿
using Dinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Dinner.Application.Services.Authentication.Queries.Login
{
    public record LoginQuery
    (
        string Email,
        string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}
