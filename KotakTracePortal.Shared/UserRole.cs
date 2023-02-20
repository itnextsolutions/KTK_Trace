using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static KotakTracePortal.Shared.CommonControls;

namespace KotakTracePortal.Shared
{
    public class UserRole
    {
        [Required]
        public string UserId { get; set; }
        public string UserName { get; set; }

        [Required]
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public List<DropDownModel> USERLIST { get; set; }
        public List<DropDownModel> ROLELIST { get; set; }
    }
}
