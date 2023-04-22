using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class Role
    {
        [AttributeCustomId]
        public int idrole { get; set; }

        [AttributeCustomUnique]
        [AttributeCustomDisplayName("Mã vai trò")]
        public string rolecode { get; set; }

        public string  rolename { get; set; }

        public string description { get; set; }
    }
}
