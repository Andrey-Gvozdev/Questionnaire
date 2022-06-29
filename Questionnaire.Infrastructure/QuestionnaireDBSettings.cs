namespace Questionnaire.Infrastructure
{
    public class QuestionnaireDBSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string QuestionsCollectionName { get; set; } = null!;

        public string QuestionDefinitionsCollectionName { get; set; } = null!;

        public string SurveysCollectionName { get; set; } = null!;
    }
}