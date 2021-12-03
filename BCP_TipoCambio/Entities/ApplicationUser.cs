using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string UserNam { get; set; }
        public string Password { get; set; }
    }
}
