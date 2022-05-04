using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaintenance.Core.Communication
{
    public class BaseResponse
    {
        public bool Success { get; protected set; }
        public int Code { get; protected set; }
        public string Message { get; protected set; }

        public BaseResponse(bool success, int code, string message)
        {
            this.Success = success;
            this.Code = code;
            this.Message = message;
        }
    }
}
