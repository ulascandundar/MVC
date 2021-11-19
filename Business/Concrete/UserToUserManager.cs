using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class UserToUserManager : IUserToUserService
    {
        private IUserToUserDal _userToUserDal;

        public UserToUserManager(IUserToUserDal userToUserDal)
        {
            _userToUserDal = userToUserDal;
        }

        public IResult FollowUser(UserToUser userToUser)
        {
            IResult result = BusinessRules.Run(CheckUserToUserExists(userToUser.FollowerId, userToUser.UserId),
                WithHerself(userToUser.FollowerId, userToUser.UserId));
            if (result!=null)
            {
                return result;
            }
            userToUser.Date = DateTime.Now;
            _userToUserDal.Add(userToUser);
            return new SuccessResult("Takip edildi");
        }

        public IDataResult<List<UserToUser>> GetAll()
        {
            var result = _userToUserDal.GetAll();
            return new SuccessDataResult<List<UserToUser>>(result);
        }

        public IResult IsFollow(int followerId, int userId)
        {
            var result = _userToUserDal.Get(u => u.FollowerId == followerId & u.UserId == userId);
            if (result == null)
            {
                return new ErrorResult("Takip etmiyor");
            }
            return new SuccessResult("Takip ediyor");
        }

        private IResult CheckUserToUserExists(int followerId,int userId)
        {
            var result = _userToUserDal.GetAll(u => u.FollowerId == followerId && u.UserId == userId).Any();
            if (result)
            {
                return new ErrorResult("Bu kullanıcıyı zaten takip ediyorsunuz.");
            }
            return new SuccessResult();
        }

        private IResult WithHerself(int followerId, int userId)
        {
            if (followerId==userId)
            {
                return new ErrorResult("Kendi kendinizi takip edemezsiniz.");
            }
            return new SuccessResult();
        }
    }
}
