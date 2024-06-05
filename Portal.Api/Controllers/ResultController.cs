using _20201132039_SinavPortali.Dtos;
using _20201132039_SinavPortali.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _20201132039_SinavPortali.Controllers
{
    [Route("api/Result")]
    [ApiController]
    [Authorize(Roles = "Admin, Student")]
    public class ResultController
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        Response _resultDto = new Response();
        AppUserOptionsController _appUserOptionsController;
        public ResultController(AppDbContext context, IMapper mapper, AppUserOptionsController appUserOptionsController)
        {
            _context = context;
            _mapper = mapper;
            _appUserOptionsController = appUserOptionsController;
        }

        [HttpGet]
        public List<ResultDto> GetAllByUser(string userId)
        {
            var results = _context.Result.Where(q => q.UserId == userId);
            var resultDtos = _mapper.Map<List<ResultDto>>(results);
            return resultDtos;
        }

        [HttpPost]
        public Response Generate(string userId, int assessmentId)
        {
            int points = 0;

            var result = new Result();
            result.Score = points;
            result.UserId = userId;
            result.AssessmentId = assessmentId;
            _context.Result.Add(result);
            _resultDto.Message = "Eklendi";
            _resultDto.Status = true;
            return _resultDto;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public Response Delete(int id)
        {
            var result = _context.Result.Where(s => s.Id == id).SingleOrDefault();
            if (result == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Not Bulunamadı!";
                return _resultDto;
            }

            _context.Result.Remove(result);
            _context.SaveChanges();
            _resultDto.Status = true;
            _resultDto.Message = "Not Silindi";
            return _resultDto;
        }
    }
}
