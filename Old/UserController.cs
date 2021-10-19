using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class UserController : Controller
    //{
    //    private readonly DbService _dbService;
    //    public UserController(DbService dbService)
    //    {
    //        _dbService = dbService;
    //    }
    //    [HttpGet]
    //    public IEnumerable<User> GetUser() 
    //    {
    //        return _dbService.Users.Include(x => x.UserData).ToList();
    //    }

    //    [HttpPost("add")]
    //    public IEnumerable<User> AddUser(UserDto dto) 
    //    {
    //        var newUser = new User{
    //                Id = Guid.NewGuid(),
    //                Username = dto.Username,
    //                Age = dto.Age
    //        };
    //        if (!string.IsNullOrEmpty(dto.PhoneNumber))
    //        {
    //            newUser.UserData = new UserData
    //            {
    //                Id = Guid.NewGuid(),
    //                PhoneNumber = dto.PhoneNumber,
    //                UserId = newUser.Id
    //            };
    //        }
            
    //        _dbService.Users.Add(newUser);
    //        _dbService.SaveChanges();
    //        return GetUser();
    //    }
    //    [HttpGet("find")]
    //    public User FindUser([FromQuery] string name)
    //    {
    //        var user = _dbService.Users
    //            .Include(x => x.UserData)
    //            .FirstOrDefault(x => x.Username == name);
    //        return user;
    //    }
    //} 
}
