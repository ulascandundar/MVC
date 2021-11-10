using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class MessageForReadUserDto:IDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int RecipientId { get; set; }
        public string RecipientName { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateRead { get; set; }
        public bool IsRead { get; set; }
    }
}
