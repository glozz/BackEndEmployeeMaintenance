using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMaintenance.WebAPI.NewFolder
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public string FullName { get; set; }
        public string EmployeeNum { get; set; }
        public string EmployedDate { get; set; }
        public string TerminatedDate { get; set; }
    }
}
