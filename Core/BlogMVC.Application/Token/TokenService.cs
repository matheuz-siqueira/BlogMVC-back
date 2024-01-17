using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogMVC.Application.Token;

public class TokenService
{
    private const string EmailAlias = "eml";  
    private readonly IConfiguration _configuration; 
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration; 
    }

    public string GenerateToken(string email)
    {
        var claims = new List<Claim>
        {
            new Claim(EmailAlias, email), 
        };   
        
        var expiration = int.Parse(_configuration.GetValue<string>("Token:Expiration"));
        var jwtKey = Encoding.UTF8.GetBytes(_configuration["Token:JwtKey"]); 
        var tokenHanlder = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(expiration), 
            SigningCredentials = 
                new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)      
        };

        var securityToken = tokenHanlder.CreateToken(tokenDescriptor); 
        return tokenHanlder.WriteToken(securityToken);
    }

    public void ValidatorToken(string token)
    {      
        var jwtKey = Encoding.UTF8.GetBytes(_configuration["Token:JwtKey"]);
        var handler = new JwtSecurityTokenHandler();
        var validators = new TokenValidationParameters
        {
            RequireExpirationTime = true, 
            IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
            ValidateIssuerSigningKey = true,
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false, 
            ValidateAudience = false 
        };

        handler.ValidateToken(token, validators, out _); 
    }
}
