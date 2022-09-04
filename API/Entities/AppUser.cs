using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        [Key]
        public Guid Id {get; set;}
        public string UserName {get; set;} = null!;
        public string FullName {get; set;} = null!;
        public string Email {get; set;} = null!;
        public byte[] PasswordHash {get; set;} = null!;
        public byte[] PasswordSalt {get; set;} = null!;
        public DateTime Created {get; set;}
    }
}