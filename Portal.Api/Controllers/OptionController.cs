using _20201132039_SinavPortali.Dtos;
using _20201132039_SinavPortali.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace _20201132039_SinavPortali.Controllers
{
    [Authorize(Roles = "Admin, Teacher, Student")]
    [Route("api/Option/[Action]")]
    [ApiController]
    public class OptionController
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        Response _resultDto = new Response();

        public OptionController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<OptionDto> GetAllByQuestion(int questionId)
        {
            var options = _context.Option.Where(o => o.QuestionId == questionId);
            var optionDtos = _mapper.Map<List<OptionDto>>(options);
            return optionDtos;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Teacher")]
        public Response AddOptionToQuestion(int questionId, OptionDto dto)
        {
            var option = _mapper.Map<Option>(dto);
            option.QuestionId = questionId;
            _context.Option.Add(option);
            _context.SaveChanges();

            _resultDto.Status = true;
            _resultDto.Message = "Seçenek Eklendi";
            return _resultDto;
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Teacher")]
        public Response Put(OptionDto dto)
        {
            var option = _context.Option.Where(o => o.Id == dto.Id).SingleOrDefault();
            if (option == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Seçenek Bulunamadı!";
                return _resultDto;
            }

            option.Text = dto.Text;
            option.IsCorrect = dto.IsCorrect;
            _context.Option.Update(option);
            _context.SaveChanges();

            _resultDto.Status = true;
            _resultDto.Message = " Seçenek Düzenledi";
            return _resultDto;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Teacher")]
        [Route("{id}")]
        public Response Delete(int id)
        {
            var option = _context.Option.Where(o => o.Id == id).SingleOrDefault();
            if (option == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Seçenek Bulunamadı!";
                return _resultDto;
            }

            _context.Option.Remove(option);
            _context.SaveChanges();
            _resultDto.Status = true;
            _resultDto.Message = " Seçenek Silindi";
            return _resultDto;
        }
    }
}
