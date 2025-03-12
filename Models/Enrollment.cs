using System.ComponentModel.DataAnnotations;
using System.Composition;

namespace SmithSwimmingSchoolApp.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; } 
        public int CourseId { get; set; }
        public int SwimmerId { get; set; }
        public int GroupingId { get; set; }       
        public Course? Course { get; set; }
        public Swimmer? Swimmer { get; set; }
        public Grouping? Grouping { get; set; }
        public ICollection<Report>? Reports { get; set; }
    }
}
