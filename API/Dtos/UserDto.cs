using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class UserDto
    {
       
        public Guid Id {get; set;}
        public string UserName {get; set;} = null!;
        public string FullName {get; set;} = null!;
        public string Email {get; set;} = null!;
        public string Token {get; set;} = null!;
        public string RefreshToken {get; set;} = null!;
        public DateTime ExpiredTime {get; set;}
        public DateTime Created {get; set;}
    }
}