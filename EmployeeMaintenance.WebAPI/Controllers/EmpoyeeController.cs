using EmployeeMaintenance.Core.Models;
using EmployeeMaintenance.Service.Interfaces;
using EmployeeMaintenance.WebAPI.NewFolder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeMaintenance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        /// <summary>
        /// Get employee list
        /// </summary>
        /// <param name=""></param>
        /// <returns>List of employee</returns>
        [HttpGet]
        public async Task<IActionResult> GetEmployeeListAsync()
        {
            try
            {
                var employeeList = _employeeService.GetEmployeesListAsync().Result;

                return Ok(copyIntoModel(employeeList));
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred fetching employee list {ex.Message}");
            }
        }

        private List<EmployeeModel> copyIntoModel(IEnumerable<Employee> employeeList)
        {
            var model = new List<EmployeeModel>();

            foreach (var item in employeeList)
            {
                var result = new EmployeeModel
                {
                    EmployeeId = item.EmployeeId,
                    PersonId = item.PersonId,
                    FullName = string.Concat(item.Person.FirstName, " ", item.Person.LastName),
                    EmployeeNum = item.EmployeeNum,
                    EmployedDate = item.EmployedDate.ToString("dd/M/yyyy"),
                    TerminatedDate = item.TerminatedDate?.ToString("dd/M/yyyy")
            };
                model.Add(result);
            }
            return model;
        }

        /// <summary>
        /// Get result for Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee detailed information</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                    return BadRequest($"Employee by Id : {id} not found");
                return Ok(getCopyIntoModel(employee));  
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred fetching employee with Id {id}, exception message : {ex.Message}");
            }
        }

        private EmployeeModel getCopyIntoModel(Employee employee)
        {
            var result = new EmployeeModel
            {
                EmployeeId = employee.EmployeeId,
                PersonId = employee.PersonId,
                FullName = string.Concat(employee.Person.FirstName, " ", employee.Person.LastName),
                EmployeeNum = employee.EmployeeNum,
                EmployedDate = employee.EmployedDate.ToString("yyyy-MM-dd"),
                TerminatedDate = employee.TerminatedDate?.ToString("yyyy-MM-dd")
            };
            return result;
        }

        /// <summary>
        /// create employee  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var employeemodel = new Employee
                {
                    PersonId = request.PersonId,
                    EmployedDate = request.EmployedDate,
                    EmployeeNum = request.EmployeeNum,
                    TerminatedDate = request.TerminatedDate
                };
                await _employeeService.CreateEmployeeAsync(employeemodel);

                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest($"failed to create employee with id :{request.EmployeeId}, exception message : {ex.Message}");
            }
        }

        /// <summary>
        /// update employee  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeModel request)
        {
            if (id > 0)
            {
                try
                {
                    var employee = await _employeeService.GetEmployeeByIdAsync(id);

                    if (employee == null)
                        return BadRequest("no data to update");

                    employee.EmployeeNum = request.EmployeeNum;
                    employee.PersonId = request.PersonId;

                    if (request.EmployedDate != "")
                        employee.EmployedDate = Convert.ToDateTime(request.EmployedDate);

                    if (request.TerminatedDate != "")
                        employee.TerminatedDate = Convert.ToDateTime(request.TerminatedDate);

                    await _employeeService.UpdateEmployeeAsync(employee);

                    return Ok("success");
                }
                catch (Exception ex)
                {
                    return BadRequest($"failed to update employee with id :{request.EmployeeId}, exception message : {ex.Message}");
                }
            }
            return BadRequest("Failed to update employee");
        }


        /// <summary>
        /// delete employee by id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                if (id != 0)
                    await _employeeService.DeleteEmployeeAsync(id);
                else
                    return BadRequest("employee id is required");

                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest($"failed to delete employee with id :{id}, exception message : {ex.Message}");
            }
        }
    }
}
