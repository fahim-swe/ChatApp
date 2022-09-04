using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class MessageParamDto
    {
        public string RecipientUsername {get; set;} = null!;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}