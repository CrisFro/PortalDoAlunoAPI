using PortalDoAluno.Application.Services;
using PortalDoAluno.Domain.Interfaces;
using PortalDoAluno.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurações dos serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7226", "http://localhost:7226")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Registrar repositórios e serviços para injeção de dependência
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IRelacionamentoService, RelacionamentoService>();
builder.Services.AddScoped<IAlunoTurmaRepository, AlunoTurmaRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowFrontend"); 
app.UseAuthorization();
app.MapControllers();
app.Run();
