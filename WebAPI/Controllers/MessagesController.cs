using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("createmessage")]
        public IActionResult CreateMessage(MessageForCreateDto messageForCreateDto)
        {
            var result = _messageService.CreateMessage(messageForCreateDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getinbox")]
        public IActionResult GetInbox(int userId)
        {
            var result = _messageService.GetInbox(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("showmessage")]
        public IActionResult ShowMessage(int messageId)
        {
            var result = _messageService.ShowMessage(messageId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("deletemessage")]
        public IActionResult DeleteMessage(int messageId)
        {
            var result = _messageService.DeleteMessage(messageId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
