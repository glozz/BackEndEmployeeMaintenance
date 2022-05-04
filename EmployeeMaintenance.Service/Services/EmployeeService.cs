using EmployeeMaintenance.Core.Communication;
using EmployeeMaintenance.Core.Enums;
using EmployeeMaintenance.Core.Models;
using EmployeeMaintenance.Core.Repositories;
using EmployeeMaintenance.Core.Uow;
using EmployeeMaintenance.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaintenance.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            this._employeeRepository = employeeRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesListAsync()
        {
            var employee = await _employeeRepository.GetEmployeesListAsync();

            if (employee == null)
            {
                return null;
            }
            return employee;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return null;
            }
            return employee;
        }

        public async Task<CreateResponse> CreateEmployeeAsync(Employee employee)
        {
            try
            {
                _employeeRepository.InsertEmployee(employee);

                await _unitOfWork.CompleteAsync();

                return new CreateResponse(true, (int)ResponseEnums.Success, nameof(ResponseEnums.Success), employee);
            }
            catch (Exception ex)
            {
                return new CreateResponse(false, (int)ResponseEnums.NotSuccess, $"Failed to create employee with Id :{employee.EmployeeId}, Exception message : {ex.Message}", null);
            }
        }

        public async Task<CreateResponse> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                _employeeRepository.UpdateEmployeeById(employee);

                await _unitOfWork.CompleteAsync();

                return new CreateResponse(true, (int)ResponseEnums.Success, nameof(ResponseEnums.Success), employee);
            }
            catch (Exception ex)
            {
                return new CreateResponse(false, (int)ResponseEnums.NotSuccess, $"Failed to update employee with Id :{employee.EmployeeId}, Exception message : {ex.Message}", null);
            }
        }

        public async Task<CreateResponse> DeleteEmployeeAsync(int id)
        {
            try
            {
                _employeeRepository.DeleteEmployeeById(id);

                await _unitOfWork.CompleteAsync();

                return new CreateResponse(true, (int)ResponseEnums.Success, nameof(ResponseEnums.Success), null);
            }
            catch (Exception ex)
            {
                return new CreateResponse(false, (int)ResponseEnums.NotSuccess, $"Failed to delete employee with Id :{id}, Exception message : {ex.Message}", null);
            }
        }
    }
}
