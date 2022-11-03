using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;
using System.Text.RegularExpressions;
using TaskAPI.Data;
using TaskAPI.Model;

namespace TaskAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {

        private readonly TskContext _dbContext;

        public OrganizationController(TskContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Organization
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganization()
        {
            if (_dbContext.Organizations == null)
            {
                

                return NotFound();
            }
            return await _dbContext.Organizations.ToListAsync();
        }

        // POST: api/Organization
        [HttpPost]
        public async Task<ActionResult<Organization>> PostOrganization(Organization org)
        {
          
            _dbContext.Organizations.Add(org);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrganization), new { id = org.Id }, org);
        }

    }
}
