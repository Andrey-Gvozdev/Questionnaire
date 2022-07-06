using AutoMapper;
using Questionnaire.Domain.Model;

namespace Questionnaire.MapProfiles;

public class SurveyMapProfile : Profile
{
    public SurveyMapProfile()
    {
        CreateMap<Survey, SurveyDto>();
    }

}
