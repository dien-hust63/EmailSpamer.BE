using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class Sender
    {
        [AttributeCustomId]
        public int idsender { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string displayname { get; set; } = "SoICT 2023";

        [AttributeCustomNotMap]
        public int sentemail { get; set; }
    }
}
