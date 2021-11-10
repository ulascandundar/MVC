using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class MessageForCreateDto:IDto
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string Text { get; set; }
        
    }
}
