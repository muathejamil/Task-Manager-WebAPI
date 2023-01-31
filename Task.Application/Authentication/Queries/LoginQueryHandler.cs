using AutoMapper;
using MediatR;
using Task.Application.Authentication.Common;
using Task.Application.Common.CustomExceptions.User;
using Task.Application.Common.Interfaces;
using Task.Application.Common.Mappers.Dtos.User;
using Task.Application.Common.Persistence;

namespace Task.Application.Authentication.Queries;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public LoginQueryHandler(
        IJwtProvider jwtProvider, 
        IUserRepository userRepository, 
        IMapper mapper)
    {
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<AuthenticationResponse> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // Validate user exists
        var dbUser =  await _userRepository.FindUserByEmailAsync(query.Email);
        if (dbUser is null)
        {
            throw new UserDoesNotExistException();
        }
        
        // Validate Password is correct
        if (query.Password != dbUser.Password) 
        {
            throw new WrongEmailOrPasswordException();            
        }
        // create JWT token
        UserDto userDto = _mapper.Map<UserDto>(dbUser);
        var token = _jwtProvider.GenerateJwtToken(userDto);
        return new AuthenticationResponse(
            userDto,
            token);
    }
}