using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchoolApp.Models
{
    public class Swimmer
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }       
        public string? IdentityUserId { get; set; }
        public ApplicationUser? IdentityUser { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
        public SwimmerSex SwimmerSex { get; set; }
    }

    public enum SwimmerSex
    {
        Male,
        Female
    }
}