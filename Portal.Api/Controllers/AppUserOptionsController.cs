using _20201132039_SinavPortali.Dtos;
using _20201132039_SinavPortali.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Generic;

namespace _20201132039_SinavPortali.Controllers
{
    [Authorize(Roles = "Admin, Student")]
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class AppUserOptionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        Response _resultDto = new Response();
        public AppUserOptionsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<Response> AddOptionToUserRelationship(string userId, int optionId)
        {
            var option = await _context.Option.AnyAsync(o => o.Id == optionId);
            if (!option)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Seçenek bulunamadı!";
                return _resultDto;
            }

            var user = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!user)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Kullanıcı bulunamadı!";
                return _resultDto;
            }
            var appUserOption = new AppUserOption { AppUserId = userId, OptionId = optionId };
            _context.AppUserOption.Add(appUserOption);
            await _context.SaveChangesAsync();
            _resultDto.Status = true;
            _resultDto.Message = "İlişki Eklendi!";
            return _resultDto;
        }

        [HttpDelete]
        public async Task<Response> DeleteOptionToUserRelationship(string userId, int optionId)
        {
            var appUserOption = await _context.AppUserOption
                .FirstOrDefaultAsync(e => e.AppUserId == userId && e.OptionId == optionId);

            var course = await _context.Option.AnyAsync(c => c.Id == optionId);
            if (appUserOption == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "İlişki bulunamadı!";
                return _resultDto;
            }

            _context.AppUserOption.Remove(appUserOption);
            await _context.SaveChangesAsync();
            _resultDto.Status = true;
            _resultDto.Message = "İlişki silindi!";
            return _resultDto;
        }

        [HttpPost("Submit")]
        public async Task<Response> SubmitExam([FromBody] ExamSubmissionDto submissionDto)
        {
            var user = await _context.Users.AnyAsync(u => u.Id == submissionDto.UserId);
            if (!user)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Kullanıcı bulunamadı!";
                return _resultDto;
            }

            foreach (var option in submissionDto.OptionId)
            {
                var appUserOption = new AppUserOption { AppUserId = submissionDto.UserId, OptionId = option };
                _context.AppUserOption.Add(appUserOption);
            }
            await _context.SaveChangesAsync();
            _resultDto.Status = true;
            _resultDto.Message = "Cevap başarıyla kaydedildi!";
            return _resultDto;
        }
    }
}
