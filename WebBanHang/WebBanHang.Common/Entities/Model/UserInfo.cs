using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.Common.Entities.Model
{
    public class UserInfo
    {
        public int iduser { get; set; }

        public string username { get; set; }

        public string usercode { get; set; }

        public string email { get; set; }

        public bool isemployee { get; set; }

        public int? branchid { get; set; }

        public string branchname { get; set; }

        public int? roleid { get; set; }

        public string rolename { get; set; }

        public int statusid { get; set; }

        public string statusname { get; set; }
    }
}
