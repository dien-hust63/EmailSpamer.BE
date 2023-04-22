using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gather.ApplicationCore.Entities
{
    public class BaseEntity
    {
        #region Property

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? createddate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string createdby { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? modifieddate { get; set; }

        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        public string modifiedby { get; set; }
        #endregion
    }
}
