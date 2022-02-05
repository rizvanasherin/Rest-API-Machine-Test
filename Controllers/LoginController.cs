using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TravelAgencyApp.Models;

namespace TravelAgencyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // 1 Dependency injection for configuration
        private readonly IConfiguration _config;

        // 2 constructor injection
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        // 3 HttpPost method

        [HttpPost("token")]
        public IActionResult Login([FromBody] UserModel user)
        {
            //check unauthorized or not
            IActionResult response = Unauthorized();

            //Authenticate user
            var loginUser = AuthenticateUser(user);

            //validate the user and generate JWT Token
            if (loginUser != null)
            {
                var tokenString = GenerateJWToken(loginUser);
                response = Ok(new { token = tokenString });
            }

            return response;
        }


        // 4 Authenticate the user
        private UserModel AuthenticateUser(UserModel user)
        {
            UserModel loginUser = null;

            //Validate the user credentials and retrieve data from db
            if (user.UserName == "Ahla")
            {
                loginUser = new UserModel
                {
                    UserName = "Ahla",
                    EmailAddress = "ahla@gmail.com",
                    DateOfJoining = new DateTime(2021, 04, 01),
                    Role = "Administrator"
                };
            }
            return loginUser;

        }

        // 5 generate token

        private string GenerateJWToken(UserModel loginUser)
        {
            //securityKey
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));


            //signing Credentials
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //claims  --Roles


            //generate token
            var token = new JwtSecurityToken
                (
                _config["Jwt:Issuer"], //header
                _config["Jwt:Issuer"], //payload
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
