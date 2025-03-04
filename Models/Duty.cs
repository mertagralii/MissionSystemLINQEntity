using System.ComponentModel.DataAnnotations;

namespace MissionSystem.Models
{
    public class Duty
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="DutyTitle boş bırakılamaz.")]
        public string DutyTitle { get; set; }

        [Required(ErrorMessage = "DutyDescription boş bırakılamaz.")]

        public string DutyDescription { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdateDate  { get; set; }

        [Required(ErrorMessage = "DutyDescription boş bırakılamaz.")]
        public DateTime DeliveryDate { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsCompleted { get; set; } = false;
    }
}
