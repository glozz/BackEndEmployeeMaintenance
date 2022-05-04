using EmployeeMaintenance.Core.Models;
using EmployeeMaintenance.Service.Interfaces;
using EmployeeMaintenance.WebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeMaintenance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            this._personService = personService;
        }

        /// <summary>
        /// Get persons list
        /// </summary>
        /// <param name=""></param>
        /// <returns>List of People</returns>
        [HttpGet()]
        public async Task<IActionResult> GetPersonsListAsync()
        {
            try
            {
                var personslist = _personService.GetPersonsListAsync().Result;

                return Ok(copyIntoModel(personslist));
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred fetching Persons list {ex.Message}");
            }
        }

        private List<PersonModel> copyIntoModel(IEnumerable<Person> person)
        {
            var personModel = new List<PersonModel>();
            foreach(var item in person)
            {
                var result = new PersonModel
                {
                    PersonId = item.PersonId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    BirthDate = item.BirthDate.ToString("yyyy-MM-dd"),
                };
                personModel.Add(result);
            }
            return personModel;
        }

        /// <summary>
        /// Get result for Person
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Person detailed information</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonByIdAsync(int id)
        {
            try
            {
                var person = await _personService.GetPersonByIdAsync(id);
                if (person == null)
                    return BadRequest($"Person by Id : {id} not found");
                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred fetching Person with Id {id}, exception message : {ex.Message}");
            }
        }

        /// <summary>
        /// Create Person  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreatePerson([FromBody] Person request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var personModel = new Person
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                };
                await _personService.CreatePersonAsync(personModel);

                return Ok("Success");
            }
            catch (Exception ex)
               {
                return BadRequest($"Failed to create person with Id :{request.PersonId}, Exception message : {ex.Message}");
            }
        }

        /// <summary>
        /// update Person  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] Person request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var person = await _personService.GetPersonByIdAsync(id);

                if (person == null)
                    return BadRequest("No data to update");

                //person.FirstName = request.FirstName;
                //person.FirstName = request.FirstName;
                //person.GradeId = request.GradeId;

                await _personService.UpdatePersonAsync(person);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update person with Id :{request.PersonId}, Exception message : {ex.Message}");
            }
        }


        /// <summary>
        /// Delete person by Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("DeletePerson/{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            try
            {
                if (id != 0)
                    await _personService.DeletePersonAsync(id);
                else
                    return BadRequest("Person id is required");

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete person with Id :{id}, Exception message : {ex.Message}");
            }
        }
    }
}