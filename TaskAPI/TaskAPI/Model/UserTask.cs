namespace TaskAPI.Model
{
    public class UserTask
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int OrgId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DeadLine { get; set; }

        public string Status { get; set; }

    
    }
}
