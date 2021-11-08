using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;
using Microsoft.EntityFrameworkCore;

using ChatAPI.Servises.Abstract;
using ChatAPI.Data.Authorize;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminUser adminUser = new AdminUser { Id = new Guid(), Username = "Admin7", Password = "@DogPas" };
        private readonly DbService _dbService;
        public AdminController(DbService dbService)
        {
            _dbService = dbService;
        }

        [Authorize]
        [Route("getlogin")]
        public IActionResult GetUsername()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [Authorize]
        [Route("getallusers")]
        public List<ChatUser> GetAllUsers()
        {
            return _dbService.ChatUsers.ToList();
        }

        [Authorize]
        [HttpGet]
        public AdminUser Index()
        {
            return adminUser;
        }
        
        [HttpPost("token")]
            public IActionResult Token(AdminUserDto dto)
            {
                var identity = GetIdentity(dto.Username, dto.Password);
                if (identity == null)
                {
                    return BadRequest(new { errorText = "Invalid username or password." });
                }
                
                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };

                return Ok(response);
            }

            private ClaimsIdentity GetIdentity(string username, string password)
            {
            AdminUser person = adminUser.Username == username && adminUser.Password == password ? adminUser : null;
                if (person != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Username)
                };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
                return null;
            }
        }
}
