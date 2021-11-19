using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IMessageService
    {
        IResult CreateMessage(MessageForCreateDto messageForCreateDto);
        IDataResult<List<MessageForReadUserDto>> GetInbox(int userId);
        IDataResult<Message> ShowMessage(int messageId);
        IResult DeleteMessage(int messageId);
    }
}
