using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class MessageManager:IMessageService
    {
        private IMessageDal _messageDal;
        private IUserDal _userDal;

        public MessageManager(IMessageDal messageDal,IUserDal userDal)
        {
            _messageDal = messageDal;
            _userDal = userDal;
        }

        public IResult CreateMessage(MessageForCreateDto messageForCreateDto)
        {
            Message message = new Message()
            {
                Text = messageForCreateDto.Text,
                SenderId = messageForCreateDto.SenderId,
                RecipientId = messageForCreateDto.RecipientId,
                DateAdded = DateTime.Now
            };
            if (_userDal.Get(u=>u.Id==message.RecipientId)==null)
            {
                return new ErrorResult("Böyle bir kullanıcı yok");
            }
            _messageDal.Add(message);
            return new SuccessResult("Mesaj gönderildi");
        }

        public IDataResult<List<MessageForReadUserDto>> GetInbox(int userId)
        {
            var result=_messageDal.GetAll(m => m.RecipientId == userId);
            List<MessageForReadUserDto> messageForReadUserDtos = new List<MessageForReadUserDto>();
            foreach (var item in result)
            {
                messageForReadUserDtos.Add(new MessageForReadUserDto()
                {
                    Id=item.Id,
                    DateAdded=item.DateAdded,
                    DateRead=item.DateRead,
                    IsRead=item.IsRead,
                    RecipientId=item.RecipientId,
                    RecipientName=_userDal.Get(u=>u.Id==item.RecipientId).FirstName,
                    SenderId=item.SenderId,
                    SenderName=_userDal.Get(u=>u.Id==item.SenderId).FirstName,
                    Text=item.Text
                });
            }
            return new SuccessDataResult<List<MessageForReadUserDto>>(messageForReadUserDtos);
           // return new SuccessDataResult<List<Message>>(result);
        }

        public IDataResult<Message> ShowMessage(int messageId)
        {
            var message = _messageDal.Get(u => u.Id == messageId);
            message.IsRead = true;
            message.DateRead = DateTime.Now;
            _messageDal.Update(message);
            return new SuccessDataResult<Message>(message);
        }
    }
}
