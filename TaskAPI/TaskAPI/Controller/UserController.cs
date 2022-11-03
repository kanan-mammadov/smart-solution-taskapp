using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskAPI.Data;
using TaskAPI.Model;

namespace TaskAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TskContext _dbContext;

        

        public UserController(TskContext dbContext)
        {
            _dbContext = dbContext;

        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            return await _dbContext.Users.ToListAsync();
        }


        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User usr)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");

            var org = await _dbContext.Organizations.FindAsync(usr.OrgId);
            if (org == null) 
                return this.StatusCode(StatusCodes.Status404NotFound, "Organization not found") ;
          

            if (org.RoleName == "ADMIN")
            {
                if (r.IsMatch(usr.Password) && usr.Password.Length<=6)
                {
                    _dbContext.Users.Add(usr);
                    await _dbContext.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetUser), new { id = usr.Id }, usr);
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, "Password must match alphanumeric characters or 6 characters");
                  

                }
            }
            else
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "Only Admin can create a user");

            
            }
        }
    }
}
