using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KotakTracePortal.Shared.CommonControls;

namespace KotakTracePortal.Shared
{
    public class UserRoleList
    {

        public string Role { get; set; }
        public string RoleId { get; set; }
        public string EMPNAME { get; set; }
        public string CREATEDBY { get; set; }
        public string CREATEDDATE { get; set; }

        public List<UserRoleList> getUserRolelist { get; set; }
    }
}
