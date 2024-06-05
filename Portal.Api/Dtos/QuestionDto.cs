using System.ComponentModel.DataAnnotations;

namespace _20201132039_SinavPortali.Dtos
{
    public class QuestionDto
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int Point { get; set; }
        public int AssessmentId { get; set; }
    }
}
