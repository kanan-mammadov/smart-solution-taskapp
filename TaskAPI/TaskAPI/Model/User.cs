using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Model
{
    public class User
    {
        public int Id { get; set; }

        public int OrgId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string  Email { get; set; }

        public string Password { get; set; }


    }
}
