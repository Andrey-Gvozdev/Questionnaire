namespace Questionnaire.Domain.Model
{
    public class Survey
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        // TODO: typo
        public string Discription { get; set; }

        public List<Question> Questions { get; set; }
    }
}
