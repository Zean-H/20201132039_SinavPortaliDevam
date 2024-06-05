using System.ComponentModel.DataAnnotations.Schema;

namespace _20201132039_SinavPortali.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public List<Question>? Questions { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public List<AppUser> Users { get; set; }
        public List<Result> Results { get; set; }
    }
}
