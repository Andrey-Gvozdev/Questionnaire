namespace Questionnaire.Domain.Model;

public class QuestionDto
{
    public string DefinitionName { get; set; }

    public string QuestionText { get; set; }

    public bool IsRequired { get; set; }
}
