using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;
using Questionnaire.Infrastructure;
using Questionnaire.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<QuestionnaireDBSettings>(builder.Configuration.GetSection("QuestionnaireDB"));

builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
builder.Services.AddTransient<IQuestionCRUDService, QuestionCRUDService>();
builder.Services.AddTransient<IQuestionDefinitionRepository, QuestionDefinitionRepository>();
builder.Services.AddTransient<IQuestionDefinitionCRUDService, QuestionDefinitionCRUDService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
