﻿using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Data;

public interface IQuestionDefinitionRepository
{
    Task<List<QuestionDefinition>> GetAllAsync();

    Task<QuestionDefinition> GetByIdAsync(Guid id);

    Task CreateAsync(QuestionDefinition newQuestionDefinition);

    Task UpdateAsync(Guid id, QuestionDefinition updatedQuestionDefinition);

    Task DeleteAsync(Guid id);
}
