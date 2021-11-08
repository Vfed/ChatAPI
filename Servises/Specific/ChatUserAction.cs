using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatAPI.Servises.Abstract;
using ChatAPI.Data.Dto;
using ChatAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

using ChatAPI.Data.Authorize;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ChatAPI.Servises.Specific
{
    public class ChatUserAction : IChatUserAction
    {
        private readonly DbService _dbService;
        public ChatUserAction(DbService dbService)
        {
            _dbService = dbService;
        }

        //add ChatUser to db via ChatUserLogicDto 

        public void AddChatUser(ChatUserLoginDto dto)
        {
            ChatUser newUser;
            if (!CheckUserExistens(dto.Username))
            {
                newUser = new ChatUser
                {
                    Id = Guid.NewGuid(),
                    Username = dto.Username,
                    ChatLists = new List<ChatsList>(),
                    Password = dto.Password,
                    Role = "user"
                };
                _dbService.ChatUsers.Add(newUser);
                _dbService.SaveChanges();
            }
            return;
        }
        
        //

        public ChatUser FindUser(ChatUserDto dto)
        {
            var user = _dbService.ChatUsers
                .FirstOrDefault(x => x.Username == dto.Username);
            return user;
        }

        // Check if ChatUser Exists 

        public bool CheckUserExistens(string username)
        {
            var user = _dbService.ChatUsers
                .FirstOrDefault(x => x.Username == username);
            return user != null;
        }

        //

        public IEnumerable<ChatUser> FindAllExeptMeUser(ChatUserDto userDto)
        {
            var user = _dbService.ChatUsers
                .Where(x => x.Username != userDto.Username);
            return user;
        }
        public IEnumerable<ChatUser> GetChatUser()
        {
            return _dbService.ChatUsers.Include(x => x.ChatLists);
        }

        //Login User -> give him Token or null
        public TokenReturn Login(ChatUserLoginDto dto)
        {
            var identity = GetIdentity(dto.Username, dto.Password);
            if (identity == null)
            {
                return null; //BadRequest
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

            TokenReturn response = new TokenReturn
            {
                Token = encodedJwt,
                UserId = identity.Name
            };

            return response;
        }

        //Check user(name,pas) give ClaimsIdentity
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            ChatUser chatUser = _dbService.ChatUsers.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (chatUser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, chatUser.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, chatUser.Role)
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
