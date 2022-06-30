using Hellang.Middleware.ProblemDetails;
using MongoDB.Driver;
using Questionnaire.Domain.CustomExceptions;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;
using Questionnaire.Infrastructure;
using Questionnaire.Infrastructure.Repository;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<QuestionnaireDBSettings>(builder.Configuration.GetSection("QuestionnaireDB"));

builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
builder.Services.AddTransient<IQuestionCrudService, QuestionCrudService>();
builder.Services.AddTransient<IQuestionDefinitionRepository, QuestionDefinitionRepository>();
builder.Services.AddTransient<IQuestionDefinitionCrudService, QuestionDefinitionCrudService>();
builder.Services.AddTransient<ISurveyRepository, SurveyRepository>();
builder.Services.AddTransient<ISurveyCrudService, SurveyCrudService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
