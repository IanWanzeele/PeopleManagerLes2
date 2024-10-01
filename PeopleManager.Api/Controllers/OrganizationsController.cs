using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrganizationsController(OrganizationService organizationService) : ControllerBase
    {
        private readonly OrganizationService _organizationService = organizationService;

        //Find (more) GET
        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var organizations = await _organizationService.Find();
            return Ok(organizations);
        }

        //Get (one) GET
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var organization = await _organizationService.Get(id);
            return Ok(organization);
        }

        //Create POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Organization organization)
        {
            var createdOrganization = await _organizationService.Create(organization);
            return Ok(createdOrganization);
        }

        //Update PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, Organization organization)
        {
            var updatedOrganization = await _organizationService.Update(id, organization);
            return Ok(updatedOrganization);
        }

        //Delete DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _organizationService.Delete(id);
            return Ok();
        }

    }
}
