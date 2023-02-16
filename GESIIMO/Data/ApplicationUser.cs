using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string NombrePersona {get;set;}

        public string ApellidosPersona { get; set; }

    }
}
