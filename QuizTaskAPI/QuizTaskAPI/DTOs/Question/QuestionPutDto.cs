using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizTaskAPI.DTOs.Question
{
    public class QuestionPutDto
    {
        public string Name { get; set; }
        public decimal Points { get; set; }
    }
}
