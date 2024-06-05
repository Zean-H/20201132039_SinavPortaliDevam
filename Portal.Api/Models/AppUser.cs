using Microsoft.AspNetCore.Identity;

namespace _20201132039_SinavPortali.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public ICollection<AppUserCourse>? AppUserCourses { get; set; }
        public ICollection<AppUserOption>? AppUserOptions { get; set; }
        public List<Result>? Results { get; set; }    
    }
}
