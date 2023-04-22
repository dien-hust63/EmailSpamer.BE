using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanHang.Common.Entities.Model
{
    public class ModulePermissionDefault
    {
        public int idmodulepermissiondefault { get; set; }

        public int idmodule { get; set; }

        public string permission { get; set; }

        public string layoutname { get; set; }
    }
}
