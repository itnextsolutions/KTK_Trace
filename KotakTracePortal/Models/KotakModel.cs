using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KotakTracePortal.Models
{
    public class KotakModel
    {
        public int Uid { get; set; }
        public String Id { get; set; }
        [Required]
        public string RequesterName { get; set; }
        [Required]

        public string Department { get; set; }
        [Required]

        public string ProjectName { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string TypeOfCollateral { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string ChannelName { get; set; }
        [Required]
        public string EDDdate { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public HttpPostedFileBase UploadFile { get; set; }

        public string UploadFileName { get; set; }
        public string UploadFileExt { get; set; }

        public string Status { get; set; }
        public string AgencyName { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public String FromDate { get; set; }

        public String ToDate { get; set; }
        [Required]
        public string ReturnRemark { get; set; }
        public List<KotakModel> ListURSModel { get; set; }
    }
}