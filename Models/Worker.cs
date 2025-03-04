namespace MissionSystem.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public bool Gender { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Adress { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdateDate { get; set; }
    }
}
