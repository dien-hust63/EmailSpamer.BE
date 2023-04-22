using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBanHang.Common.Entities.Model
{
    public class BlobStorage
    {
        [Display(Name = "File Name")]
        public string FileName
        {
            get;
            set;
        }
        [Display(Name = "File Size")]
        public string FileSize
        {
            get;
            set;
        }
        public string Modified
        {
            get;
            set;
        }
    }
}
