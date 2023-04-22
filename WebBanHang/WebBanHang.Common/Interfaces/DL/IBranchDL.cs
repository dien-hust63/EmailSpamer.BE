using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Common.Interfaces.DL
{
    public interface IBranchDL : IBaseDL<Branch>
    {
        public List<Branch> getBrancByUser(string email);
    }
}
