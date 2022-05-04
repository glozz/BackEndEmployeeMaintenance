using EmployeeMaintenance.Core.Communication;
using EmployeeMaintenance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaintenance.Service.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetPersonsListAsync();
        Task<Person> GetPersonByIdAsync(int id);
        Task<CreateResponse> CreatePersonAsync(Person person);
        Task<CreateResponse> UpdatePersonAsync(Person person);
        Task<CreateResponse> DeletePersonAsync(int id);
    }
}
