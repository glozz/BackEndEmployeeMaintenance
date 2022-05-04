using EmployeeMaintenance.Core.Communication;
using EmployeeMaintenance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaintenance.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesListAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<CreateResponse> CreateEmployeeAsync(Employee employee);
        Task<CreateResponse> UpdateEmployeeAsync(Employee employee);
        Task<CreateResponse> DeleteEmployeeAsync(int id);
    }
}
