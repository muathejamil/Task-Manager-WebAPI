using MediatR;
using Task.Application.Authentication.Common;

namespace Task.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName, string LastName, string Email, string Password) : IRequest<AuthenticationResponse>;