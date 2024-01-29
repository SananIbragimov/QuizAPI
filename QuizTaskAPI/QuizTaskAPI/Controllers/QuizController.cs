using Microsoft.AspNetCore.Mvc;
using QuizTaskAPI.Data;
using QuizTaskAPI.DTOs.Quiz;

namespace QuizTaskAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public QuizController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var quiz = _dbContext.Quizzes.Select(
                q => new QuizGetDto
                {
                    Name = q.Name,
                    CreationDate = q.CreationDate
                })
                .ToList();

            return Ok(quiz);
        }
    }
}
