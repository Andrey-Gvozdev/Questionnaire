using AutoMapper;
using Hellang.Middleware.ProblemDetails;
using Questionnaire.Domain.CustomExceptions;
using Questionnaire.Domain.Data;
using Questionnaire.Domain.Services.CRUDServices;
using Questionnaire.Domain.Services.ValidationServices;
using Questionnaire.Infrastructure;
using Questionnaire.Infrastructure.Repository;
using Questionnaire.MapProfiles;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<QuestionnaireDBSettings>(builder.Configuration.GetSection("QuestionnaireDB"));

builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
builder.Services.AddTransient<IQuestionCrudService, QuestionCrudService>();
builder.Services.AddTransient<IQuestionValidationService, QuestionValidationService>();
builder.Services.AddTransient<IQuestionDefinitionRepository, QuestionDefinitionRepository>();
builder.Services.AddTransient<IQuestionDefinitionCrudService, QuestionDefinitionCrudService>();
builder.Services.AddTransient<IQuestionDefinitionValidationService, QuestionDefinitionValidationService>();
builder.Services.AddTransient<ISurveyRepository, SurveyRepository>();
builder.Services.AddTransient<ISurveyCrudService, SurveyCrudService>();
builder.Services.AddTransient<ISurveyValidationService, SurveyValidationService>();

builder.Services.AddControllers(opt => opt.SuppressAsyncSuffixInActionNames = false);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg => 
{ 
    cfg.AddProfile<QuestionMapProfile>();
    cfg.AddProfile<QuestionDefinitionMapProfile>();
    cfg.AddProfile<SurveyMapProfile>();
});
builder.Services.Configure<IMapper>(cfg => cfg.ConfigurationProvider.AssertConfigurationIsValid());
builder.Services.AddProblemDetails(options =>
{
    options.MapToStatusCode<NotFoundException>(StatusCodes.Status404NotFound);
    options.MapToStatusCode<ValidationException>(StatusCodes.Status400BadRequest);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseProblemDetails();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
