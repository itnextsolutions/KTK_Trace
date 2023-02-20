using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotakTracePortal.Shared
{
    public class Login
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
        public string RedirectURLEncoded { get; set; }
        public string DML { get; set; }
        public string Select { get; set; }
    }
}
