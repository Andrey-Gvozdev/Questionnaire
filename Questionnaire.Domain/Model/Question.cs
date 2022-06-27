namespace Questionnaire.Domain.Model
{
    internal class Question
    {
        public Guid Id { get; set; }

        public QuestionDefinition Definition { get; set; }

        public string QuestionText { get; set; }

        public bool IsRequared { get; set; }
    }
}
