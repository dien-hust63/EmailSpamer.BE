using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;

namespace WebBanHang.Common.Entities.Model
{
    public class Receiver
    {
        [AttributeCustomId]
        public int idreceiver { get; set; }

        public string email { get; set; }
    }
}
