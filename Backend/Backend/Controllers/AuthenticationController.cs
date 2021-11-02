using Backend.Models;
using Backend.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/auth")]
    public class AuthenticationController : ApiController
    {
        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody]User user)
        {
            if(string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Enter your username and password");
            }

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var existingUser = context.Users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
                    if (existingUser != null)
                    {
                        return Ok(CreateToken(existingUser));
                    }
                    return BadRequest("Provide provide a valid user name and password");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("register")]
        [HttpPost]
        public IHttpActionResult Register([FromBody] User user)
        {
            try {
                using (var context = new ApplicationDbContext())
                {
                    var exists = context.Users.Any(u => u.UserName == user.UserName);
                    if (exists) return BadRequest("User already exists");

                    context.Users.Add(user);
                    context.SaveChanges();

                    return Ok(CreateToken(user));
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private JwtToken CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Email, user.UserName)
            });

           // const string secretKey = Constants.SECRET_KEY; //"4ece00eb880f4bfd92b8a3aec95e6ada";
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Constants.SECRET_KEY));
            var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = (JwtSecurityToken)tokenHandler.CreateJwtSecurityToken(
                    subject: claims,
                    signingCredentials: signinCredentials                    
                );

            var tokenString = tokenHandler.WriteToken(token);

            return new JwtToken()
            {
                UserName = user.UserName,
                Token = tokenString,
                Name = user.FirstName
            };
        }

    }

    public class JwtToken
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}
