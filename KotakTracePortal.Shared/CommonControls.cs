using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KotakTracePortal.Shared
{
    public class CommonControls
    {

        public class DropDownModel
        {
            public string Id { get; set; }
            public string Description { get; set; }
            public string Identifier { get; set; }
            public string Buyerid { get; set; }
        }

        public class DropDownListModel
        {
            public DropDownListModel() { }
            public List<DropDownModel> DropDownList { get; set; }
            public List<SelectListItem> FinalList { get; set; }
            public string drpId { get; set; }
            public string drpText { get; set; }
            public string SelectedText { get; set; }
            public bool? MulitiSelect { get; set; }
            public string ChangeEvent { get; set; }
        }

      
    }
}
