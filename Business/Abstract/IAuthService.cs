using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult PasswordReset(string email, string password,string fav);

        IResult PasswordResetNoFav(int id, string password);

        IResult Update(UserForUpdateDto userForUpdateDto);

        IResult UpdateNoPassword(UserForUpdateDto userForUpdateDto);

        IResult PassowordRefresh(string email);
    }
}
