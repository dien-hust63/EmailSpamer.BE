using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.DBHelper;

namespace WebBanHang.Common.ServiceCollection
{
    public class CoreServicesCollection
    {
        IServiceProvider _iServiceProvider;
        IDBHelper _dbHelper;
        public CoreServicesCollection(IServiceProvider iServiceProvider)
        {
            _iServiceProvider = iServiceProvider;
        }

    }
}
