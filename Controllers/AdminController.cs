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
        private readonly List<AdminUser> adminUsers = new List<AdminUser>() {
            new AdminUser{ Id = new Guid(), Username = "Admin7", Password = "12345" },
            new AdminUser{ Id = new Guid(), Username = "Admin008", Password = "12345" }
        };

        private readonly AdminUser adminUser = new AdminUser { Id = new Guid(), Username = "Admin7", Password = "12345" };
        private readonly DbService _dbService;
        private readonly IHttpContextAccessor _contextAccessor;
        public AdminController(DbService dbService, IHttpContextAccessor contextAccessor)
        {
            _dbService = dbService;
            _contextAccessor = contextAccessor;
        }

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

        //public int GetUserId() =>
        //    Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value ?? "0");

        //public string GetUserRole() =>
        //    _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

        [Authorize]
        [HttpGet("gettokens")]
        public IActionResult WorkWithTokens()
        {
            return Ok(_contextAccessor.HttpContext.User.Claims.ToList());
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
            AdminUser person = adminUsers.Find(x => x.Username == username && x.Password == password);
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
