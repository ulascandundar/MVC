using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("Kullanucı bulunamadı");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Şifre yaalış");
            }

            return new SuccessDataResult<User>(userToCheck, "Giriş yapıldı");
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = userForRegisterDto.Status,
                Fav = userForRegisterDto.Fav.ToLower(),
                Path=userForRegisterDto.Path
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, "Üye olundu");
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) == null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        public IResult PasswordReset(string email,string password,string fav)
        {
            var user = _userService.GetByMail(email);
            if (user.Fav!=fav)
            {
                return new ErrorResult("Soruyu yanlış cevapladınız");
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userService.Update(user);
            return new SuccessResult();
        }

        public IResult PasswordResetNoFav(int id, string password)
        {
            var user = _userService.GetById(id).Data;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userService.Update(user);
            return new SuccessResult();
        }

        public IResult PassowordRefresh(string email)
        {
            var user = _userService.GetByMail(email);
            Random rd = new Random();
            string world = "ASDL7.JQXKJ123ULASNAC4589VW";
            string newPassword = "";
            for (int i = 0; i < 6; i++)
            {
                newPassword += world[rd.Next(world.Length)];
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            var result=_userService.Update(user);
            if (result.Success)
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("mvccodeeps@gmail.com");
                message.To.Add(email);
                message.Subject = "Şifre Değişimi";
                message.Body = "Yeni şifreniz:" + newPassword;
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("mvccodeeps@gmail.com", "");
                client.Port = 587;

                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Send(message);
                return new SuccessResult("Şifre değiştirildi");
            }
            return new ErrorResult();
        }

        public IResult Update(UserForUpdateDto userForUpdateDto)
        {
            var user = _userService.GetById(userForUpdateDto.Id).Data;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForUpdateDto.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.FirstName = userForUpdateDto.Email;
            user.Status = userForUpdateDto.Status;
            user.LastName = userForUpdateDto.LastName;
            user.Fav = userForUpdateDto.Fav;
            _userService.Update(user);
            return new SuccessResult();
        }

        public IResult UpdateNoPassword(UserForUpdateDto userForUpdateDto)
        {
            var user = _userService.GetById(userForUpdateDto.Id).Data;
            user.FirstName = userForUpdateDto.FirstName;
            user.LastName = userForUpdateDto.LastName;
            user.Email = userForUpdateDto.Email;
            _userService.Update(user);
            return new SuccessResult();
        }

        
    }
}
