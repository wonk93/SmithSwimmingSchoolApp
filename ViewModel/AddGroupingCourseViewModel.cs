using Microsoft.AspNetCore.Mvc.Rendering;
using SmithSwimmingSchoolApp.Models;

namespace SmithSwimmingSchoolApp.ViewModel
{
    public class AddGroupingCourseViewModel
    {
        public Course? Course { get; set; }
        public Grouping? Grouping { get; set; }
        public SelectList? CourseList { get; set; }
    }
}
