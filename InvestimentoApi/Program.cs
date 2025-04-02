

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
var app = builder.Build();

app.UseStaticFiles(); // Permite servir arquivos HTML, CSS e JS
app.UseDefaultFiles(); // Permite acessar index.html diretamente

app.UseCors("PermitirTudo");
app.UseAuthorization();
app.MapControllers();

app.Run();
