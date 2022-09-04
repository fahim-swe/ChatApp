using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public class MessageFilter :PaginationFilter
    {
        public string SenderUsername {get; set;} = null!;
        public string RecipientUsername {get; set;} = null!;
    }
}