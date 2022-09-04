using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Message
    {
        [Key]
        public Guid Id {get; set;}

        public string SenderUsername {get; set;} = null!;
        public string RecipientUsername {get; set;} = null!;
        public string Content {get; set;} = null!;


        public DateTime MessageSent {get;protected set;} = DateTime.Now;
        public DateTime MessageRead {get; set;}
    }
}