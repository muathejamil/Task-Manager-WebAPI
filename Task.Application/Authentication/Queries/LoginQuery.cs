using MediatR;
using Task.Application.Authentication.Common;

namespace Task.Application.Authentication.Queries;

public record LoginQuery(string Email, string Password) 
    : IRequest<AuthenticationResponse>;

