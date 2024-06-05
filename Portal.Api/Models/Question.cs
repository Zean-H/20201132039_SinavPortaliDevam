namespace _20201132039_SinavPortali.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }

        public int Point {  get; set; }

        public List<Option>? Options { get; set; }
    }
}
