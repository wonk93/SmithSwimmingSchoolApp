using Microsoft.AspNetCore.Mvc.Rendering;
using SmithSwimmingSchoolApp.Models;

namespace SmithSwimmingSchoolApp.ViewModel
{
    public class SwimmerInCourseViewModel
    {
        public Course? Course { get; set; }

        public Swimmer? Swimmer { get; set; }       

    }
}
