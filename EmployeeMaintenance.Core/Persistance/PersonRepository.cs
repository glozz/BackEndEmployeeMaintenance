using EmployeeMaintenance.Core.Models;
using EmployeeMaintenance.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMaintenance.Core.Persistance
{
    public class PersonRepository : IPersonRepository
    {

        private readonly ApplicationDbContext _dbContext;
        protected internal DbSet<Person> _dbSet;

        public PersonRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<Person>();
        }

        public async Task<IEnumerable<Person>> GetPersonsListAsync()
        {
            return await _dbSet.AsQueryable().AsNoTracking().ToListAsync();
        }

        public void InsertPerson(Person person)
        {
            var dbEntityEntry = _dbContext.Entry(person);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                _dbSet.Add(person);
            }
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void UpdatePersonById(Person person)
        {
            _dbSet.Update(person);
        }

        public void DeletePersonById(int id)
        {
            var person = _dbSet.Find(id);
            _dbSet.Remove(person);
        }
    }
}
