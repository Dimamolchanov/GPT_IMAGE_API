using GPT_IMAGE_API.Business;
using GPT_IMAGE_API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<OpenAiCreateImageCompletionClient>(x => new RestApiOpenAiCreateImageCompletionClient(builder.Configuration.GetSection("OPENAI_API_KEY").Value));
builder.Services.AddSingleton<CreateCompletionDataProvider>(x => new CreateImageCompletionDataProvider(x.GetService<OpenAiCreateImageCompletionClient>(), new GPT_IMAGE_API.Utils.FileSaver()));

builder.Services.AddSingleton<OpenAiEditImageCompletionClient>(x => new RestApiOpenAiEditImageCompletionClient(builder.Configuration.GetSection("OPENAI_API_KEY").Value));
builder.Services.AddSingleton<EditCompletionDataProvider>(x => new EditImageCompletionDataProvider(x.GetService<OpenAiEditImageCompletionClient>(), new GPT_IMAGE_API.Utils.FileSaver()));

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
