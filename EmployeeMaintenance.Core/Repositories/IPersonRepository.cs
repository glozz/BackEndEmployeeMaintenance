using EmployeeMaintenance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaintenance.Core.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPersonsListAsync();
        void InsertPerson(Person person);
        Task<Person> GetPersonByIdAsync(int id);
        void UpdatePersonById(Person person);
        void DeletePersonById(int id);
    }
}
