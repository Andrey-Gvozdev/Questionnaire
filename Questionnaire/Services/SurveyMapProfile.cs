using AutoMapper;
using Questionnaire.Domain.Model;

namespace Questionnaire.Services;

public class SurveyMapProfile : Profile
{
    public SurveyMapProfile()
    {
        CreateMap<Survey, SurveyDto>();
    }

}
