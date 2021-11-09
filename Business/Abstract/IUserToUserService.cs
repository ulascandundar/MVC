using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserToUserService
    {
        IResult FollowUser(UserToUser userToUser);
        IDataResult<List<UserToUser>> GetAll();

        
    }
}
