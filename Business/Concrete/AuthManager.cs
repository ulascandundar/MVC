using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
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
                Fav = userForRegisterDto.Fav.ToLower()
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
    }
}
