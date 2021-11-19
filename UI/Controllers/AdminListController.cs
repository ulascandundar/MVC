using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class AdminListController : Controller
    {
        IUserService _userService;
        IAuthService _authService;

        public AdminListController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public IActionResult Index()
        {
            var datas = _userService.GetAll(null).Data;
            
            
            return View(datas);
        }

        public IActionResult UserAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserAdd(UserForRegisterDto userForRegisterDto)
        {
            _authService.Register(userForRegisterDto);
            return RedirectToAction("Index");
        }
        public IActionResult UserGet(int id)
        {
            var u = _userService.GetById(id).Data;
            UserForUpdateDto userForLoginDto = new UserForUpdateDto()
            {
                Id=u.Id,
                Email = u.Email,
                Fav = u.Fav,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Password = "",
                Status = u.Status
            };
            
            return View(userForLoginDto);
        }

        [HttpPost]
        public IActionResult UserUpdate(UserForUpdateDto p)
        {
            UserForUpdateDto us = new UserForUpdateDto()
            {
                Id= p.Id,
                Email = p.Email,
                Fav = p.Fav,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Password = p.Password,
                Status = p.Status
            };
            _authService.Update(us);
            return RedirectToAction("Index");
        }
    }
}
