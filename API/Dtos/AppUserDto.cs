using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AppUserDto
    {
        public Guid Id {get; set;}
        public string UserName {get; set;} = null!;
        public string FullName {get; set;} = null!;
        public string Email {get; set;} = null!;
        public DateTime Created {get; set;}
    }
}