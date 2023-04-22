using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public  class RoleModuleCustom
    {
        [AttributeCustomId]
        public int idrolemodule { get; set; }

        /// <summary>
        /// id role
        /// </summary>
        public int idrole { get; set; }

        /// <summary>
        /// id module
        /// </summary>
        public int idmodule { get; set; }

        /// <summary>
        /// danh sách quyền
        /// </summary>
        public List<Permission> permission { get; set; }
    }
}
