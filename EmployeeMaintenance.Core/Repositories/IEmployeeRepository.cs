using EmployeeMaintenance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaintenance.Core.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesListAsync();
        void InsertEmployee(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(int id);
        void UpdateEmployeeById(Employee employee);
        void DeleteEmployeeById(int id);
    }
}
