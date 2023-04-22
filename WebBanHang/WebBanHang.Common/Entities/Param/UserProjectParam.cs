using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gather.ApplicationCore.Entities.Param
{
    public class UserProjectParam:PagingRequest
    {
        /// <summary>
        /// text search
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Project Id
        /// </summary>
        public Guid ProjectId { get; set; }
    }
}
