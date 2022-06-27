namespace Questionnaire.Domain.Model
{
    internal class Survey
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Discription { get; set; }

        public List<Question> Questions { get; set; }
    }
}
