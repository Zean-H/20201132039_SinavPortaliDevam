namespace _20201132039_SinavPortali.Dtos
{
    public class ExamSubmissionDto
    {
        public int AssessmentId { get; set; }
        public string UserId { get; set; }
        public List<int> OptionId { get; set; }
    }
}
