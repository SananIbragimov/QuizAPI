using Microsoft.AspNetCore.Mvc;
using QuizTaskAPI.Data;
using QuizTaskAPI.DTOs.Question;

namespace QuizTaskAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public QuestionController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int? id, QuestionPutDto dto)
        {
            var question = _dbContext.Questions.FirstOrDefault(x => x.Id == id);
            if (question == null) return NotFound();

            var newDto = new QuestionPutDto
            {
                Name = dto.Name,
                Points = dto.Points
            };

            return Ok(newDto);
        }
    }
}
