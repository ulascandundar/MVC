using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class LoginController : Controller
    {
        IAuthService _authService;
        IUserService _userService;

        public LoginController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;

        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.error = false;
            ViewBag.deactive = false;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(UserForLoginDto userForLoginDto)
        {
            var result = _authService.Login(userForLoginDto).Success;
            if (result)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,userForLoginDto.Email)
                };
                var result1= _userService.GetByMail(userForLoginDto.Email).Status;
                if (result1==false)
                {
                    ViewBag.error = false;
                    ViewBag.deactive = true;
                    return View();
                }
                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                ViewBag.user = userForLoginDto.Email;
                return RedirectToAction("Index", "Product");
              

            }
            ViewBag.deactive = false;
            ViewBag.error = true;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult PasswordReset()
        {
            ViewBag.Result2 = false;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult PasswordReset(string email,string fav)
        {
            if (_authService.UserExists(email).Success)
            {
                Random rd = new Random();
                string world = "ASDL7.JQXKJ123ULASNAC4589VW";
                string newPassword = "";
                for (int i = 0; i < 6; i++)
                {
                    newPassword += world[rd.Next(world.Length)];
                }
                var fav1 = fav.ToLower();
                var result=_authService.PasswordReset(email, newPassword,fav1);
                if (!result.Success)
                {
                    ViewBag.war = "Soruyu yanlış cevapladınız";
                    ViewBag.Result2 = true;
                    
                    return View();
                }
                MailMessage message = new MailMessage();
                message.From = new MailAddress("mvccodeeps@gmail.com");
                message.To.Add(email);
                message.Subject = "Şifre Değişimi";
                message.Body = "Yeni şifreniz:" + newPassword;
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("mvccodeeps@gmail.com", "MvcCodeeps123");
                client.Port = 587;
                
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Send(message);

                //MailMessage mail = new MailMessage();
                //mail.From = new MailAddress("mvccodeeps@gmail.com");
                //mail.To.Add(email);
                //mail.Subject = "Yeni şifre";
                //mail.Body = "Şifreniz:" + newPassword;
                //WebMail.SmtpServer = "smtp.gmail.com";
                //WebMail.EnableSsl = true;
                //WebMail.UserName = "mvccodeeps@gmail.com";
                //WebMail.Password = "MvcCodeeps123";
                //WebMail.SmtpPort = 587;
                //WebMail.Send(email, "Yeni şifre", "Şifreniz:" + newPassword);

                TempData["mail"] = email;
                ViewBag.Result2 = false;
                return RedirectToAction("ConfirmMail", "Login");
            }
            ViewBag.Result2 = true;
            ViewBag.Result1 = "Böyle Bir Mail yok.";
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ConfirmMail()
        {
            return View();
        }

    }
}
