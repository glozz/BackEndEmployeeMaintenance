using EmployeeMaintenance.Core.Models;
using EmployeeMaintenance.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeMaintenance.Core.Persistance
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        protected internal DbSet<Employee> _dbSet;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<Employee>();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesListAsync()
        {
            return await _dbSet.Include(x => x.Person).AsNoTracking().ToListAsync();
        }

        public void InsertEmployee(Employee employee)
        {
            var dbEntityEntry = _dbContext.Entry(employee);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                _dbSet.Add(employee);
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _dbSet.Include(i => i.Person).AsNoTracking()
            .FirstOrDefaultAsync(i => i.EmployeeId == id);
        }

        public void UpdateEmployeeById(Employee employee)
        {
            _dbSet.Update(employee);
        }

        public void DeleteEmployeeById(int id)
        {
            var employee = _dbSet.Find(id);
            _dbSet.Remove(employee);
        }

    }
}
