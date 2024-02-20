namespace QuizTaskAPI.DTOs.Option
{
    public class OptionPostDto
    {
        public string Name { get; set; }
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
    }
}
