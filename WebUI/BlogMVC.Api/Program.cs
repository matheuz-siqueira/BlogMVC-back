using BlogMVC.Api.Filter;
using BlogMVC.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration); 
builder.Services.AddInfrastructureSwagger();
builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionsFilter)));
builder.Services.AddScoped<AuthenticatedUserAttribute>();
var app = builder.Build();

app.UseCors(policy => {
    policy.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
