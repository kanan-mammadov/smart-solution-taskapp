using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using TaskAPI.Data;
using TaskAPI.Model;

namespace TaskAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly TskContext _dbContext;

        public UserTaskController(TskContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/UserTask
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTask>>> GetUserTask()
        {
            if (_dbContext.Tasks == null)
            {
                return NotFound();
            }
            return await _dbContext.Tasks.ToListAsync();
        }


        public static void SendEmail(string bodyText,string fromEmail,string ToEmail)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(fromEmail);
                message.To.Add(new MailAddress(ToEmail));
                message.Subject = "New Task";
                message.IsBodyHtml = true; 
                message.Body = bodyText;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ToEmail, "******");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }

        // GET: api/UserTask/1
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTask>> GetUserTask(int id)
        {
            if (_dbContext.Tasks == null)
            {
                return NotFound();
            }
            var tsk = await _dbContext.Tasks.FindAsync(id);

            if (tsk == null)
            {
                return NotFound();
            }

            return tsk;
        }


        // POST: api/UserTask
        [HttpPost]
        public async Task<ActionResult<UserTask>> PostUserTask(UserTask tsk)
        {

           

            var userInfo = await _dbContext.Users.FindAsync(tsk.UserId);
            var OrgInfo = await _dbContext.Organizations.FindAsync(tsk.OrgId);
            if (OrgInfo ==null) return this.StatusCode(StatusCodes.Status404NotFound, "Organization not found");
           if (userInfo != null) {

                _dbContext.Tasks.Add(tsk);
                await _dbContext.SaveChangesAsync();
                SendEmail("Task assign to you", "mail@info.com", userInfo.Email);
            return CreatedAtAction(nameof(GetUserTask), new { id = tsk.Id }, tsk);
            }
            else
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "User not found");
            }


        }
    }
}
