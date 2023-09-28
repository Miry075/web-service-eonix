using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using miry.manage_personne.business.AsyncServices;
using miry.manage_personne.business.Dtos;
using miry.manage_personne.business.Dtos.PersonneDtos;
using miry.manage_personne.business.IServices;


namespace miry.manage_personne.api.Controllers
{
    [ApiController]
    [Route("api/personnes")]
    public class PersonneController : ControllerBase
    {
        private readonly IPersonneService _personneService;
        private readonly IPersonneAsyncService _personneAsyncService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PersonneController> _logger;

        public PersonneController(IPersonneService personneService, IPersonneAsyncService personneAsyncService, IConfiguration configuration, ILogger<PersonneController> logger) {
            _personneService = personneService;
            _personneAsyncService = personneAsyncService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetPersonneById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PersonneReadDto> GetPersonneById(Guid id)
        {
            try {
                return Ok(_personneService.GetDataById(id));
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet( Name = "GetPersonnesByFilter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<PersonneReadDto>> GetPersonnes(string? keyName, string? keyFirstname)
        {
            try
            {
                return Ok(_personneService.FindPersonnes(keyName, keyFirstname));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost(Name = "CreatePersonne")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonneReadDto>> CreatePersonne(PersonneCreateDto personne)
        {
            if(personne == null)
            {
                return BadRequest();
            }
            var result = await _personneService.Save(personne);
            try {
                _personneAsyncService.PublishData(result, _configuration["NewPersonneEventName"]);
            } catch (Exception ex){
                _logger.LogError($" --> Could not publish asynchronously: {ex.Message}");
            }
            return CreatedAtAction(nameof(GetPersonneById), new { id = result.Id }, result);
        }

        [HttpPut("{id}", Name ="UpdatePersonne" )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePersonne(Guid id, PersonneUpdateDto personne)
        {
            var inDb = await _personneService.GetDataById(id);
            if(inDb == null)
            {
                return NotFound();
            }
            var result = await _personneService.Update(personne);
            return result ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}", Name ="DeletePersonne")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePersonne(Guid id)
        {
            
            try {
                var result = await _personneService.Remove(id);
                return result ? NoContent() : BadRequest();
            } catch (ArgumentNullException) {
                return NotFound();
            } catch {
                return BadRequest();
            }
            
        }
    }
}

