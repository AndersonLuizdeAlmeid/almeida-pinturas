using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Users.Application.Authentications.JwtToken.Services;
public class TokenService
{
    public static string GenerateCustomToken(long id, bool isActive)
    {
        var issuer = "https://almeida-pinturas.site";
        var audience = "local-api";

        var claims = new List<Claim>
    {
        new("userID", id.ToString()),
        new("isAuthenticated", isActive.ToString())
    };

        var key = new SymmetricSecurityKey(Convert.FromBase64String(Key.Secret!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.UtcNow.AddHours(27),
            signingCredentials: creds,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static TokenDto DecryptToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Key.Secret!)),
            ValidateIssuer = true,
            ValidIssuer = "https://almeida-pinturas.site",

            ValidateAudience = true,
            ValidAudience = "local-api",

            RequireExpirationTime = true,
            ValidateLifetime = false,
            ClockSkew = TimeSpan.FromMinutes(5)
        };

        var principal = handler.ValidateToken(token, validationParameters, out var validatedToken);
        var jwtToken = validatedToken as JwtSecurityToken;

        TokenDto tokenDto = new TokenDto
        {
            ExpirationDate = jwtToken != null ? jwtToken.ValidTo : DateTime.MinValue,
            IsAuthenticated = principal.Claims.Any(c => c.Type == "isAuthenticated" && bool.Parse(c.Value))
        };

        return tokenDto;
    }
}