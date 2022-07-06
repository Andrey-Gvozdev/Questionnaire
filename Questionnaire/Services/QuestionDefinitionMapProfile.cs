﻿using AutoMapper;
using Questionnaire.Domain.Model;

namespace Questionnaire.Services;

public class QuestionDefinitionMapProfile : Profile
{
    public QuestionDefinitionMapProfile()
    {
        CreateMap<QuestionDefinition, QuestionDefinitionDto>().ForMember(q => q.UIType, opt => opt.MapFrom(q => q.UIType.ToString()));
    }
}
