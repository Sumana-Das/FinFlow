using UserService.Models;
using System.Collections.Concurrent;

namespace UserService.Services;

public class UserService : IUserService
{
    private readonly ITokenService _tokenService;
    private static ConcurrentDictionary<string, User> _users = new ConcurrentDictionary<string, User>();
    
    public UserService (ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    /// <summary>
    /// Registers a new user with hashing password using BCrypt 
    /// and adding the user in in-memory dictionary
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public bool Register (RegisterRequest request)
    {
        var user = new User
        {
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = request.Role
        };

        return _users.TryAdd(user.Username, user);
    }

    /// <summary>
    /// Logs in an existing user by checking in the dictionary 
    /// then generating token after verifying using BCrypt
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public string? Login (LoginRequest request)
    {
        if(_users.TryGetValue(request.Username, out var user) && BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return _tokenService.GenerateToken(user);
        }

        return null;
    }
}