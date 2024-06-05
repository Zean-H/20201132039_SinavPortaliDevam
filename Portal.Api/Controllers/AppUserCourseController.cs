using _20201132039_SinavPortali.Dtos;
using _20201132039_SinavPortali.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _20201132039_SinavPortali.Controllers
{
    [Authorize(Roles = "Admin, Student")]
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class AppUserCourseController : Controller
    {
        private readonly AppDbContext _context;

        Response _resultDto = new Response();

        public AppUserCourseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<Response> AddCourseToUserRelationship(RelationDto dto)
        {
            var course = await _context.Course.AnyAsync(o => o.Id == dto.CourseId);
            if (!course)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Seçenek bulunamadı!";
                return _resultDto;
            }

            var user = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!user) 
            {
                _resultDto.Status = false;
                _resultDto.Message = "Kullanıcı bulunamadı!";
                return _resultDto;
            }
            var appUserCourse = new AppUserCourse { AppUserId = dto.UserId, CourseId = dto.CourseId };
            _context.AppUserCourse.Add(appUserCourse);
            await _context.SaveChangesAsync();
            _resultDto.Status = true;
            _resultDto.Message = "İlişki Eklendi!";
            return _resultDto;
        }

        [HttpDelete]
        public async Task<Response> DeleteCourseToUserRelationship(RelationDto dto)
        {
            var appUserCourse = await _context.AppUserCourse
                .FirstOrDefaultAsync(e => e.AppUserId == dto.UserId && e.CourseId == dto.CourseId);

            var course = await _context.Course.AnyAsync(c => c.Id == dto.CourseId);
            if (appUserCourse == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "İlişki bulunamadı!";
                return _resultDto;
            }

            _context.AppUserCourse.Remove(appUserCourse);
            await _context.SaveChangesAsync();
            _resultDto.Status = true;
            _resultDto.Message = "İlişki silindi!";
            return _resultDto;
        }

    }
}
