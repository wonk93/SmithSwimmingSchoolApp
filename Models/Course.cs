using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchoolApp.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; } 
        public string? Title { get; set; }
        public int CoachId { get; set; }       
        public Coach? Coach { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }

        public LevelCourse LevelCourse { get; set; }
    }

    public enum LevelCourse
    {
        Junior,
        Gilder,
        Senior,
        Pro
    }
}
