using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatAPI.Data.Models;
using ChatAPI.Data.Dto;
using ChatAPI.Servises.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ChatsListController : ControllerBase
    {
        private readonly IChatsListAction _chatslistAction;
        public ChatsListController(IChatsListAction chatslistAction)
        {
            _chatslistAction = chatslistAction;
        }

        [HttpGet]
        public IEnumerable<ChatsList> GetUserChats()
        {
            return _chatslistAction.GetUserChats();
        }
        [HttpGet("gettime")]
        public DateTime GetCurrentDataChats(Guid userId,Guid chatId)
        {
            return _chatslistAction.GetCurrentDataChats(userId,chatId);
        }
    }
}
