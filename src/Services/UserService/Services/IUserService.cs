using UserService.Models;

namespace UserService.Services;

public interface IUserService
{
    bool Register(RegisterRequest request);

    string? Login(LoginRequest request);
}