namespace Questionnaire.Domain.Model;

public class Survey
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public List<Question> Questions { get; set; }
}
