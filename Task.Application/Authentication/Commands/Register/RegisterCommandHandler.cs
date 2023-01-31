using AutoMapper;
using MediatR;
using Task.Application.Authentication.Common;
using Task.Application.Common.CustomExceptions.User;
using Task.Application.Common.Interfaces;
using Task.Application.Common.Mappers.Dtos.User;
using Task.Application.Common.Persistence;
using Task.Domain.Common.Entities;


namespace Task.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : 
    IRequestHandler<RegisterCommand, AuthenticationResponse>
{
    
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public RegisterCommandHandler(
        IJwtProvider jwtProvider, 
        IUserRepository userRepository, 
        IMapper mapper)
    {
        _mapper = mapper;
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
    }
    
    public async Task<AuthenticationResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Validate the user doesn't already exist
        var dbUser = await _userRepository.FindUserByEmailAsync(command.Email);
        if (dbUser is not null)
        {
            throw new DuplicateEmailException();
        }
        
        // Create user & persist to database
        var user = _mapper.Map<User>(command);
        
        //TODO: Hash password here
        
        await _userRepository.CreateUserAsync(user);

        var userDto = _mapper.Map<UserDto>(user);

        // Create Token
        var token = _jwtProvider.GenerateJwtToken(userDto);
        
        return new AuthenticationResponse(
            userDto,
            token);
    }
}