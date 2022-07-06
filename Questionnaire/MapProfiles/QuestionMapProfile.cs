using AutoMapper;
using Questionnaire.Domain.Model;

namespace Questionnaire.MapProfiles;

public class QuestionMapProfile : Profile
{
    public QuestionMapProfile()
    {
        CreateMap<Question, QuestionDto>().ForMember(q => q.DefinitionName, opt => opt.MapFrom(q => q.Definition.Name));
    }
}
