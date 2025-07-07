using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserService.Helpers;
using UserService.Services;

namespace UserService.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtAuth (this IServiceCollection services, IConfiguration config)
    {
        // Get the jwt section from the appconfig and then configure it in the services
        var jwtSection = config.GetSection("JwtSettings");
        services.Configure<JwtSettings>(jwtSection);

        // the the values from jwtSection and put those in settings and get the key from secret in the appconfig
        var settings = jwtSection.Get<JwtSettings>();
        var key = settings != null ? Encoding.UTF8.GetBytes(settings.Secret) : null;

        // validate the token with the secret
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = settings != null ? settings.Issuer : null,
                    ValidAudience = settings != null ? settings.Audience : null,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, Services.UserService>();

        return services;
    }
}