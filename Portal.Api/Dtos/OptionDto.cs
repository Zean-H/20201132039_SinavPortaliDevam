using System.ComponentModel.DataAnnotations;

namespace _20201132039_SinavPortali.Dtos
{
    public class OptionDto
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

    }

}
