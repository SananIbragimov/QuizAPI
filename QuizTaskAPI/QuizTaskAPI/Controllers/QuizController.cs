using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizTaskAPI.Data;
using QuizTaskAPI.DTOs.Quiz;
using QuizTaskAPI.Entities;
using QuizTaskAPI.Validators.QuizValidator;
using System.Security.Claims;

namespace QuizTaskAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<QuizPostDto> _quizPostDtoValidator;

        public QuizController(AppDbContext dbContext, IMapper mapper, IValidator<QuizPostDto> quizPostDtoValidator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _quizPostDtoValidator = quizPostDtoValidator;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roles = User.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();
            Console.WriteLine($"UserId: {userId}");
            Console.WriteLine($"Roles: {string.Join(", ", roles)}");

            var quizzes = _dbContext.Quizzes.Include(x=>x.Questions).ThenInclude(x=>x.Options).ToList();
            var quizDtos = _mapper.Map<List<QuizGetDto>>(quizzes);

            return Ok(quizDtos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetById(int id)
        {
            var quiz = _dbContext.Quizzes.Include(q => q.Questions).ThenInclude(q => q.Options)
                .FirstOrDefault(q => q.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            var quizDto = _mapper.Map<QuizDetailedGetDto>(quiz);

            return Ok(quizDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post(QuizPostDto dto)
        {

            var quiz = _mapper.Map<Quiz>(dto);
            _dbContext.Quizzes.Add(quiz);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int? id, QuizPutDto dto)
        {
            var quiz = _dbContext.Quizzes.FirstOrDefault(x => x.Id == id);
            if (quiz == null) return NotFound();

            quiz.Name = dto.Name;
            _dbContext.SaveChanges();


            return Ok(new {QuizId = quiz.Id});
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var quiz = _dbContext.Quizzes.Include(q => q.Questions).FirstOrDefault(q => q.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            _dbContext.Quizzes.Remove(quiz);
            _dbContext.SaveChanges();

            return NoContent();
        }

    }
}
