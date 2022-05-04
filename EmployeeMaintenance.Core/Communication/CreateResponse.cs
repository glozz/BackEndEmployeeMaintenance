using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaintenance.Core.Communication
{
    public class CreateResponse : BaseResponse
    {
        public object Object { get; private set; }

        public CreateResponse(bool success, int code, string message, object obj) : base(success, code, message)
        {
            Object = obj;
        }
    }
}
