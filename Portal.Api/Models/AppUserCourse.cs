namespace _20201132039_SinavPortali.Models
{
    public class AppUserCourse
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
