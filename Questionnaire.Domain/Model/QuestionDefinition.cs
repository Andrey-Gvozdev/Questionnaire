namespace Questionnaire.Domain.Model
{
    public class QuestionDefinition
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public QuestionDefinitionType Type { get; set; }

        public QuestionDefinitionUIType UIType { get; set; }

        public object Validation { get; set; }
    }

    public enum QuestionDefinitionType
    {

    }

    public enum QuestionDefinitionUIType
    {

    }
}