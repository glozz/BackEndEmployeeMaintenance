using EmployeeMaintenance.Core.Communication;
using EmployeeMaintenance.Core.Enums;
using EmployeeMaintenance.Core.Models;
using EmployeeMaintenance.Core.Repositories;
using EmployeeMaintenance.Core.Uow;
using EmployeeMaintenance.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeMaintenance.Service.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            this._personRepository = personRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Person>> GetPersonsListAsync()
        {
            var person = await _personRepository.GetPersonsListAsync();

            if (person == null)
            {
                return null;
            }
            return person;
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);
            if (person == null)
            {
                return null;
            }
            return person;
        }

        public async Task<CreateResponse> CreatePersonAsync(Person person)
        
        {
            try
            {
                _personRepository.InsertPerson(person);

                await _unitOfWork.CompleteAsync();

                return new CreateResponse(true, (int)ResponseEnums.Success, nameof(ResponseEnums.Success), person);
            }
            catch (Exception ex)
             {
                return new CreateResponse(false, (int)ResponseEnums.NotSuccess, $"Failed to create new person :{person.FirstName + person.LastName}, Exception message : {ex.Message}", null);
            }
        }

        public async Task<CreateResponse> UpdatePersonAsync(Person person)
        {
            try
            {
                _personRepository.UpdatePersonById(person);

                await _unitOfWork.CompleteAsync();

                return new CreateResponse(true, (int)ResponseEnums.Success, nameof(ResponseEnums.Success), person);
            }
            catch (Exception ex)
            {
                return new CreateResponse(false, (int)ResponseEnums.NotSuccess, $"Failed to update person with Id :{person.PersonId}, Exception message : {ex.Message}", null);
            }
        }

        public async Task<CreateResponse> DeletePersonAsync(int id)
        {
            try
            {
                _personRepository.DeletePersonById(id);

                await _unitOfWork.CompleteAsync();

                return new CreateResponse(true, (int)ResponseEnums.Success, nameof(ResponseEnums.Success), null);
            }
            catch (Exception ex)
            {
                return new CreateResponse(false, (int)ResponseEnums.NotSuccess, $"Failed to delete person with Id :{id}, Exception message : {ex.Message}", null);
            }
        }
    }

}