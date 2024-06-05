namespace _20201132039_SinavPortali.Models
{
    public class AppUserOption
    {
        public int OptionId { get; set; }
        public Option Option { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
