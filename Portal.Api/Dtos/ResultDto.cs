using System.ComponentModel.DataAnnotations;

namespace _20201132039_SinavPortali.Dtos
{
    public class ResultDto
    {
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }
        public int AssessmentId { get; set; }

        public string UserId { get; set; }
    }
}
