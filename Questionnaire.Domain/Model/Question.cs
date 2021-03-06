namespace Questionnaire.Domain.Model;

public class Question
{
    public Guid Id { get; set; }

    public QuestionDefinition Definition { get; set; }

    public string QuestionText { get; set; }

    public bool IsRequired { get; set; }
}
