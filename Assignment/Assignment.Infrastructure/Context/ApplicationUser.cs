using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public override string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public virtual DateTime? LastLoginTime { get; set; } = DateTime.Now;
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
