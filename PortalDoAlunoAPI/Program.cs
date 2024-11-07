using PortalDoAluno.Application.Services;
using PortalDoAluno.Domain.Interfaces;
using PortalDoAluno.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurações dos serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração de CORS (opcional, necessário apenas se o frontend acessar a API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:5002") // Ajuste para a URL do seu frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Registrar repositórios e serviços para injeção de dependência
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();
builder.Services.AddScoped<ITurmaService, TurmaService>();

var app = builder.Build();

// Configuração do pipeline HTTP
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowFrontend"); // Adicione isto, se configurou CORS
app.UseAuthorization();
app.MapControllers();
app.Run();