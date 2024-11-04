using PortalDoAluno.Application.Services;
using PortalDoAluno.Domain.Interfaces;
using PortalDoAluno.Infrastructure.Respositories;

var builder = WebApplication.CreateBuilder(args);

// Configura��es dos servi�os
builder.Services.AddControllers(); // Adiciona suporte para controladores (API REST)

// Configura��es do Swagger para documenta��o da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar reposit�rios e servi�os para inje��o de depend�ncia
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAlunoService, AlunoService>();

var app = builder.Build();

// Configura��o do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redireciona requisi��es HTTP para HTTPS

app.UseAuthorization(); // Middleware de autoriza��o

app.MapControllers(); // Mapeia os controladores para os endpoints

app.Run();
