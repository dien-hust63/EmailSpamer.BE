using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class Module
    {
        [AttributeCustomId]
        public int idmodule { get; set; }

        [AttributeCustomUnique]
        [AttributeCustomDisplayName("Mã phân hệ")]
        public string layoutcode { get; set; }

        public string  layoutname { get; set; }

        /// <summary>
        /// id phân hệ cha
        /// </summary>
        public int? parentid { get; set; }

        /// <summary>
        /// là phân hệ cha
        /// </summary>
        public int? isparent { get; set; }

        /// <summary>
        /// sắp xếp
        /// </summary>
        public int? sortorder { get; set; }

        /// <summary>
        /// tên router
        /// </summary>
        public string routename { get; set; }

        /// <summary>
        /// icon
        /// </summary>
        public string icon { get; set; }


    }
}
