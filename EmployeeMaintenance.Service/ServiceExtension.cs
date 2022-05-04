using EmployeeMaintenance.Core;
using EmployeeMaintenance.Core.Persistance;
using EmployeeMaintenance.Core.Repositories;
using EmployeeMaintenance.Core.Uow;
using EmployeeMaintenance.Service.Interfaces;
using EmployeeMaintenance.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

namespace EmployeeMaintenance.Service
{
    public static class ServiceExtension
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IEmployeeService, EmployeeService>();


            return services;
        }
    }
}
