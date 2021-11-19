using Business.Abstract;
using Entities.Concrete;
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
    public class UserToUserController : ControllerBase
    {
        IUserToUserService _userToUser;

        public UserToUserController(IUserToUserService userToUser)
        {
            _userToUser = userToUser;
        }
        [HttpPost("follow")]
        public IActionResult Follow(int userId, int followerId)
        {
            UserToUser userToUser = new UserToUser();
            userToUser.UserId = userId;
            userToUser.FollowerId = followerId;
            var result = _userToUser.FollowUser(userToUser);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
                return Ok(_userToUser.GetAll());
            
        }

        [HttpGet("isFollow")]
        public IActionResult IsFollow(int followerId, int userId)
        {
            var result = _userToUser.IsFollow(followerId, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
    }
}
