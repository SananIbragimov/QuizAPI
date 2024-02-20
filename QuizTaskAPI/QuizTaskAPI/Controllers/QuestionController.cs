using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizTaskAPI.Data;
using QuizTaskAPI.DTOs.Question;
using QuizTaskAPI.DTOs.Quiz;
using QuizTaskAPI.Entities;

namespace QuizTaskAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuestionController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(QuestionPostDto dto)
        {
            var question = _mapper.Map<Question>(dto);
            _dbContext.Questions.Add(question);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int? id, QuestionPutDto dto)
        {
            var question = _dbContext.Questions.FirstOrDefault(x => x.Id == id);
            if (question == null) return NotFound();

            question.Name = dto.Name;
            question.Points = dto.Points;
            _dbContext.SaveChanges();

            var updateQuestionDto = _mapper.Map<QuestionPutDto>(question);
            return Ok(updateQuestionDto);
        }
    }
}
