using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CreateMessageDto
    {
        public string RecipientUsername {get; set;} = null!;
        public string Content {get; set;} = null!;
    }
}