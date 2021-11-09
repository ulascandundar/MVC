using Business.Abstract;
using Core.Utilities.Results;
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
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll(int userId)
        {
            var result = _userService.GetAll(userId);
            List<UserForProfileDto> profiles = new List<UserForProfileDto>();
            foreach (var item in result.Data)
            {
                profiles.Add(new UserForProfileDto()
                {

                    Id=item.Id,
                    Email=item.Email,
                    FirstName=item.FirstName,
                    LastName=item.LastName,
                    Path=item.Path
                });
            }
            SuccessDataResult<List<UserForProfileDto>> proff = new SuccessDataResult<List<UserForProfileDto>>(profiles);
            if (result.Success)
            {
                return Ok(proff);
            }
            return BadRequest(profiles);
        }

        [HttpGet("get")]
        public IActionResult Get(int id)
        {
            var result = _userService.GetById(id).Data;
            UserForProfileDto profile = new UserForProfileDto()
            {
                Id = result.Id,
                Email = result.Email,
                FirstName=result.FirstName,
                LastName=result.LastName,
                Path=result.Path
            };
            SuccessDataResult<UserForProfileDto> prof = new SuccessDataResult<UserForProfileDto>(profile);
            if (prof.Success)
            {
                return Ok(prof);
            }
            return BadRequest(prof);
        }

        [HttpGet("getfollowers")]
        public IActionResult GetFollowers(int id)
        {
            var result = _userService.GetFollowers(id);
            List<UserForProfileDto> profiles = new List<UserForProfileDto>();
            foreach (var item in result.Data)
            {
                profiles.Add(new UserForProfileDto()
                {

                    Id = item.Id,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Path = item.Path
                });
            }
            SuccessDataResult<List<UserForProfileDto>> proff = new SuccessDataResult<List<UserForProfileDto>>(profiles);
            if (result.Success)
            {
                return Ok(proff);
            }
            return BadRequest(profiles);
        }

        [HttpGet("getfollowing")]
        public IActionResult GetFollowing(int id)
        {
            var result = _userService.GetFollowing(id);
            List<UserForProfileDto> profiles = new List<UserForProfileDto>();
            foreach (var item in result.Data)
            {
                profiles.Add(new UserForProfileDto()
                {

                    Id = item.Id,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Path = item.Path
                });
            }
            SuccessDataResult<List<UserForProfileDto>> proff = new SuccessDataResult<List<UserForProfileDto>>(profiles);
            if (result.Success)
            {
                return Ok(proff);
            }
            return BadRequest(profiles);
        }

        //[HttpGet("getfollowers")]
        //public IActionResult
    }
}
