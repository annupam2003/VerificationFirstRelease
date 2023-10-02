using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Verification.EntityModel;

namespace Verification.Controller.Users;

[Route("Users/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration configuration;

    public LoginController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    [HttpPost("[action]")]
    public IActionResult SingIn([FromBody] UserInfo userinfo)
    {
        if (!(userinfo.UserId.Equals("a") && userinfo.Password.Equals("a")))
        {
            return Unauthorized();
        }
        else
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt").GetSection("Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
                {
                  new Claim(JwtRegisteredClaimNames.Sub, userinfo.UserId),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim("UserId", userinfo.UserId),
                };
            var token = new JwtSecurityToken(issuer: configuration.GetSection("Jwt").GetSection("Issuer").Value, configuration.GetSection("Jwt").GetSection("Issuer").Value, claims, expires: Expires(), signingCredentials: credentials);
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }

    DateTime Expires()
    {
        DateTime ExpireDateis = DateTime.Now.AddMinutes(20);
        try
        {
            var ExpiryTime = (Convert.ToInt32(configuration.GetSection("Jwt").GetSection("ExpiryTime").Value));
            var ExpiryWithIn = configuration.GetSection("Jwt").GetSection("ExpiryWithIn").Value.ToString();
            switch (ExpiryWithIn)
            {
                case "Year":
                    ExpireDateis = DateTime.Now.AddYears(ExpiryTime);
                    break;
                case "Month":
                    ExpireDateis = DateTime.Now.AddMonths(ExpiryTime);
                    break;
                case "Day":
                    ExpireDateis = DateTime.Now.AddDays(ExpiryTime);
                    break;
                case "Hour":
                    ExpireDateis = DateTime.Now.AddHours(ExpiryTime);
                    break;
                case "Minute":
                    ExpireDateis = DateTime.Now.AddMinutes(ExpiryTime);
                    break;
                case "Second":
                    ExpireDateis = DateTime.Now.AddSeconds(ExpiryTime);
                    break;
            }
            return ExpireDateis;
        }
        catch
        {
            return ExpireDateis;
        }


    }
}
