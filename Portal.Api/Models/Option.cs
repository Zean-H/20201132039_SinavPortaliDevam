namespace _20201132039_SinavPortali.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public ICollection<AppUserOption>? AppUserOptions { get; set; }
    }
}
