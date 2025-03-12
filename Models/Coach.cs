using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchoolApp.Models
{
    public class Coach
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }       
        public string? IdentityUserId { get; set; }
        public ApplicationUser? IdentityUser { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public CoachSex CoachSex { get; set; }
    }

    public enum CoachSex
    {
        Male,
        Female
    }
}