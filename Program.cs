using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjetoDBZ.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddScoped<ProjetoDBZ.Interfaces.IPersonagemService, ProjetoDBZ.Services.PersonagemService>();

// 1. Definindo a política de segurança
builder.Services.AddCors(options =>
{
    options.AddPolicy("MinhaPoliticaCors", policy =>
    {
        policy.AllowAnyOrigin() // Em produção, aqui você colocaria o site oficial
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MinhaPoliticaCors");

app.UseAuthorization();


// --- O SEGREDO ESTÁ AQUI ---
app.UseDefaultFiles(); // 1º: Procura o index.html na wwwroot
app.UseStaticFiles();  // 2º: Libera o acesso aos arquivos (css, js, html)
// ---------------------------

app.MapControllers();
app.Run();
