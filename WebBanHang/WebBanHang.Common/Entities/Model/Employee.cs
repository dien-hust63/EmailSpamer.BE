using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.Common.Entities.Model
{
    public class Employee
    {
        [AttributeCustomId]
        public int idemployee { get; set; }
        [AttributeCustomUnique]
        [AttributeCustomDisplayName("Mã nhân viên")]
        public string employeecode { get; set; }

        public string employeename{ get; set; }
        [AttributeCustomUnique]
        [AttributeCustomDisplayName("Email")]
        public string email { get; set; }

        public string password { get; set; }

        public int branchid { get; set; }

        public string branchname { get; set; }

        public int statusid { get; set; }

        public string statusname { get; set; }

        public int roleid { get; set; }

        public string rolename { get; set; }
    }
}
