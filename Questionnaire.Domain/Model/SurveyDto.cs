namespace Questionnaire.Domain.Model;

public class SurveyDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public List<QuestionDto> Questions { get; set; }
}
