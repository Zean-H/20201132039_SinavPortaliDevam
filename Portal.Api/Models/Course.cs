namespace _20201132039_SinavPortali.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<AppUserCourse>? AppUserCourses { get; set; }
        public List<Assessment>? Assessments { get; set; }
    }
}
