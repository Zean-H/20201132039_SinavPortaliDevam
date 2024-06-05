using Microsoft.EntityFrameworkCore;

namespace _20201132039_SinavPortali.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int Score { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }
    }
}
