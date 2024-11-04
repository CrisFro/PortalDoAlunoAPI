using PortalDoAluno.Application.Services;
using PortalDoAluno.Domain.Interfaces;
using PortalDoAluno.Infrastructure.Respositories;

var builder = WebApplication.CreateBuilder(args);

// Configurações dos serviços
builder.Services.AddControllers(); // Adiciona suporte para controladores (API REST)

// Configurações do Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar repositórios e serviços para injeção de dependência
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAlunoService, AlunoService>();

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redireciona requisições HTTP para HTTPS

app.UseAuthorization(); // Middleware de autorização

app.MapControllers(); // Mapeia os controladores para os endpoints

app.Run();
