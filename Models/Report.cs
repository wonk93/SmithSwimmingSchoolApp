using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchoolApp.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; } 
        public string? Content { get; set; } 
        public int EnrollmentId { get; set; }      
        public Enrollment? Enrollment { get; set; }
    }
}
