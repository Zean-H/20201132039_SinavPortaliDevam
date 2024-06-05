using System.ComponentModel.DataAnnotations;

namespace _20201132039_SinavPortali.Dtos
{
    public class CourseDto
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

    }
}
