﻿using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll(int? userId);
        IDataResult<User> GetById(int userId);
        IResult Add(User user);

        List<OperationClaim> GetClaims(User user);

        User GetByMail(string email);

        IResult Update(User user);

        IDataResult<List<User>> GetFollowers(int id);
        IDataResult<List<User>> GetFollowing(int id);
    }
}
