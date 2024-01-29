using Microsoft.AspNetCore.Mvc;
using QuizTaskAPI.Data;
using QuizTaskAPI.DTOs.Option;
using QuizTaskAPI.DTOs.Question;

namespace QuizTaskAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class OptionController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public OptionController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int? id, OptionPutDto dto)
        {
            var option = _dbContext.Options.FirstOrDefault(x => x.Id == id);
            if (option == null) return NotFound();

            var newDto = new OptionPutDto
            {
                Name = dto.Name,
                IsCorrect = dto.IsCorrect

            };

            return Ok(newDto);
        }
    }
}

