// PersonsController.cs
using Microsoft.AspNetCore.Mvc;
using PersonManagerFieldTech.Services.Services.Interfaces;
using PersonManagerFieldTech.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PersonManagerFieldTech.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonsController : ControllerBase
    {
        private static List<PersonViewModel> _persons = new List<PersonViewModel>();

        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersons()
        {
            return Ok(await _personService.GetPersons());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var person = await _personService.GetPersonById(id);
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonViewModel person)
        {
            if (person == null || !IsValidPerson(person))
                return BadRequest();

            await _personService.CreatePerson(person);

            return CreatedAtAction(nameof(GetPersonById), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonViewModel updatedPerson)
        {
            var existingPerson = await _personService.GetPersonById(id);
            if (existingPerson == null || !IsValidPerson(updatedPerson))
                return BadRequest();

            return Ok(await _personService.UpdatePerson(id, updatedPerson));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _personService.GetPersonById(id);
            if (person == null)
                return NotFound();

            var res = await _personService.DeletePerson(id);

            if (!res)
                return BadRequest();

            return NoContent();
        }

        // Validation method
        private bool IsValidPerson(PersonViewModel person)
        {
            return !string.IsNullOrWhiteSpace(person.FirstName)
                && !string.IsNullOrWhiteSpace(person.LastName)
                && person.Age > 0;
        }
    }

}
