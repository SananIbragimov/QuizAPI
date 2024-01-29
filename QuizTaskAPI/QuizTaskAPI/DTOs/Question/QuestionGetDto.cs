using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuizTaskAPI.DTOs.Option;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizTaskAPI.DTOs.Question
{
    public class QuestionGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Points { get; set; }

        public List<OptionGetDto> Options { get; set; }
    }
}
