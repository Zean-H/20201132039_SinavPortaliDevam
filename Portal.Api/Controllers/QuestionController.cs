using _20201132039_SinavPortali.Dtos;
using _20201132039_SinavPortali.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _20201132039_SinavPortali.Controllers
{
    [Authorize(Roles = "Admin, Teacher, Student")]
    [Route("api/Question/[Action]")]
    [ApiController]
    public class QuestionController
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        Response _resultDto = new Response();

        public QuestionController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{assessmentId}")]
        public List<QuestionDto> GetAllFromAssessment(int assessmentId)
        {
            var questions = _context.Question.Where(q => q.AssessmentId == assessmentId);
            var questionDtos = _mapper.Map<List<QuestionDto>>(questions);
            return questionDtos;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Teacher")]
        public Response AddQuestionToAssessment(int assessmentId, QuestionDto dto)
        {
            var question = _mapper.Map<Question>(dto);
            question.AssessmentId = assessmentId;
            _context.Question.Add(question);
            _context.SaveChanges();

            _resultDto.Status = true;
            _resultDto.Message = "Soru Eklendi";
            return _resultDto;
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Teacher")]
        public Response Put(QuestionDto dto)
        {
            var question = _context.Question.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (question == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Soru Bulunamadı!";
                return _resultDto;
            }

            question.Text = dto.Text;
            _context.Question.Update(question);
            _context.SaveChanges();

            _resultDto.Status = true;
            _resultDto.Message = "Soru Düzenledi";
            return _resultDto;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Teacher")]
        [Route("{id}")]
        public Response Delete(int id)
        {
            var question = _context.Question.Where(s => s.Id == id).SingleOrDefault();
            if (question == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Soru Bulunamadı!";
                return _resultDto;
            }

            _context.Question.Remove(question);
            _context.SaveChanges();
            _resultDto.Status = true;
            _resultDto.Message = "Soru Silindi";
            return _resultDto;
        }
    }
}
