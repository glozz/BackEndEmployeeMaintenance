//using Microsoft.AspNetCore.Mvc;

//namespace EmployeeMaintenance.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class PersonController : ControllerBase
//    {

//        private readonly IEmployeeMaintenanceService _employeeMaintenanceService;

//        public PersonController(IEmployeeMaintenanceService employeeMaintenanceService)
//        {
//            this._employeeMaintenanceService = employeeMaintenanceService;

//        }

//        /// <summary>
//        /// Get persons list
//        /// </summary>
//        /// <param name=""></param>
//        /// <returns>List of People</returns>
//        [HttpGet(Name = "GetWeatherForecast")]
//        [HttpGet()]
//        public async Task<IActionResult> GetPersonsAsync()
//        {
//            try
//            {
//                var personslist = _employeeMaintenanceService.GetAllPeopleAsync().Result;

//                return Ok(personslist);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"An error occurred fetching Persons list {ex.Message}");
//            }
//        }

//        /// <summary>
//        /// Get result for Person
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns>Person detailed information</returns>
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetPersonByIdAsync(int id)
//        { 
//            try
//            {
//                var person = await _employeeMaintenanceService.GetPersonByIdAsync(id);
//                if (person == null)
//                    return BadRequest($"Person by Id : {id} not found");
//                return Ok(person);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"An error occurred fetching Person with Id {id}, exception message : {ex.Message}");
//            }
//        }

//        /// <summary>
//        /// Create Person  
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        [HttpPost()]
//        public async Task<IActionResult> CreatePerson([FromBody] Person request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            try
//            {
//                var personModel = new Person
//                {
//                    Id = request.PersonId,
//                    FirstName = request.FirstName,
//                    LastName = request.LastName,
//                    BirthDate = request.BirthDate,
//                };
//                await _employeeMaintenanceService.CreatePersonAsync(personModel);

//                return Ok("Success");
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Failed to create person with Id :{request.Id}, Exception message : {ex.Message}");
//            }
//        }

//        /// <summary>
//        /// update Person  
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdatePerson(int id, [FromBody] Person request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            try
//            {
//                var person = await _employeeMaintenanceService.GetPersonByIdAsync(id);

//                if (person == null)
//                    return BadRequest("No data to update");

//                person.FirstName = request.FirstName;
//                person.FirstName = request.FirstName;
//                person.GradeId = request.GradeId;

//                await _employeeMaintenanceService.UpdatePersonAsync(person);

//                return Ok("Success");
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Failed to update person with Id :{request.Id}, Exception message : {ex.Message}");
//            }
//        }


//        /// <summary>
//        /// Delete person by Id
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        [HttpDelete()]
//        [Route("DeletePerson/{id}")]
//        public async Task<IActionResult> DeletePerson([FromRoute] int id)
//        {
//            try
//            {
//                if (id != 0)
//                    await _employeeMaintenanceService.DeleteStudentAsync(id);
//                else
//                    return BadRequest("Person id is required");

//                return Ok("Success");
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Failed to delete person with Id :{id}, Exception message : {ex.Message}");
//            }
//        }
//    }
//}