using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gather.ApplicationCore.Entities
{
    public class ServiceResult
    {
        public int Code { get; set; } = 200;

        public bool Success { get; set; } = true;

        public object Data { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public void setError(string errorMessage)
        {
            this.Success = false;
            this.Data = false;
            this.ErrorCode = "VD500";
            this.ErrorMessage = errorMessage;
        }
    }
}
