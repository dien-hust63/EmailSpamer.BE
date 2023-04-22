using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class RoleModule
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
        public string permission { get; set; }

        /// <summary>
        /// tên module
        /// </summary>
        [AttributeCustomNotMap]
        public string modulename { get; set; }

        /// <summary>
        /// tên module
        /// </summary>
        [AttributeCustomNotMap]
        public bool isparent { get; set; }


        /// <summary>
        /// tên module
        /// </summary>
        [AttributeCustomNotMap]
        public int parentid { get; set; }

        /// <summary>
        /// tên module
        /// </summary>
        [AttributeCustomNotMap]
        public string icon { get; set; }

        /// <summary>
        /// tên module
        /// </summary>
        [AttributeCustomNotMap]
        public string routename { get; set; }

        /// <summary>
        /// tên module
        /// </summary>
        [AttributeCustomNotMap]
        public int sortorder { get; set; }

        [AttributeCustomNotMap]
        public string modulecode { get; set; }


    }
}
