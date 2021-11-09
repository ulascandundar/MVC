using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IUserToUserDal _userToUserDal;
        public UserManager(IUserDal userDal, IUserToUserDal userToUserdal)
        {
            _userDal = userDal;
            _userToUserDal = userToUserdal;
        }
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult("Eklendi");
        }

        public IDataResult<List<User>> GetAll(int userId)
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(i => i.Id != userId), "Listelendi");
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId), "Listelendi");
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public IDataResult<List<User>> GetFollowers(int id)
        {
            var result = _userToUserDal.GetAll(u => u.UserId == id).Select(u => u.FollowerId);
            List<User> users = new List<User>();
            foreach (int item in result)
            {
                users.Add(_userDal.Get(u => u.Id == item));
            }
            return new SuccessDataResult<List<User>>(users);
        }

        public IDataResult<List<User>> GetFollowing(int id)
        {
            var result = _userToUserDal.GetAll(u => u.FollowerId == id).Select(u => u.UserId);
            List<User> users = new List<User>();
            foreach (int item in result)
            {
                users.Add(_userDal.Get(u => u.Id == item));
            }
            return new SuccessDataResult<List<User>>(users);
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult("güncellendi");
        }
    }
}
