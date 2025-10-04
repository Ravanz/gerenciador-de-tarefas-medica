using Microsoft.EntityFrameworkCore;
using Serilog;
using vSaude.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day);
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// TODO: Estudar implementação de Redis para cache
// - Instalar pacote: Microsoft.Extensions.Caching.StackExchangeRedis
// - Adicionar serviço Redis no docker-compose.yml
// - Configurar: builder.Services.AddStackExchangeRedisCache(options => { ... })
// - Usar IDistributedCache nos controllers para cachear listas de tarefas
// - Definir tempo de expiração (ex: 5 minutos para listas)
// - Invalidar cache ao criar/atualizar/deletar tarefas

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<TarefaCreateValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "vSaúde API",
        Version = "v1",
        Description = "API para gerenciamento de tarefas médicas"
    });
});

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var maxRetries = 10;
    var delay = TimeSpan.FromSeconds(3);
    
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            logger.LogInformation("Conectando ao banco... ({Attempt}/{MaxRetries})", i + 1, maxRetries);
            db.Database.EnsureCreated();
            logger.LogInformation("Banco conectado com sucesso!");
            break;
        }
        catch (Exception ex) when (i < maxRetries - 1)
        {
            logger.LogWarning(ex, "Falha na conexão. Tentando novamente em {Delay}s...", delay.TotalSeconds);
            Thread.Sleep(delay);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Não foi possível conectar após {MaxRetries} tentativas.", maxRetries);
            throw;
        }
    }
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsync("{\"title\":\"Erro inesperado\",\"status\":500}");
    });
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
