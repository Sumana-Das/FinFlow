using Microsoft.AspNetCore.Identity;

namespace UserService.Models;

public class User
{
    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public Role Role { get; set; }
}