using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchoolApp.Models
{
    public class Grouping
    {
        [Key]
        public int Id { get; set; } 
        public string? Level { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; } 
        public int Places { get; set; }        
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
