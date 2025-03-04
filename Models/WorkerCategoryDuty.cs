namespace MissionSystem.Models
{
    public class WorkerCategoryDuty
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public int CategoryId { get; set; }

        public int DutyId { get; set; }

        public Worker Worker { get; set; }

        public Category Category { get; set; }

        public Duty Duty { get; set; }
    }
}
