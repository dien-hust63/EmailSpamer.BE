using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;

namespace WebBanHang.Common.Entities.Param
{
    public class ProductDetailParam
    {
        public string product { get; set; }

        public string productdetail { get; set; }

        public List<IFormFile> file { get; set; }
    }
}
