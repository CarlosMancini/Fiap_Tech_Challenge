using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
var servidor = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? configuration.GetSection("MassTransit")["Servidor"] ?? string.Empty;
var usuario = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? configuration.GetSection("MassTransit")["Usuario"] ?? string.Empty;
var senha = Environment.GetEnvironmentVariable("RABBITMQ_PASS") ?? configuration.GetSection("MassTransit")["Senha"] ?? string.Empty;

builder.Services.AddMassTransit((x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(servidor, "/", h =>
        {
            h.Username(usuario);
            h.Password(senha);
        });

        cfg.ConfigureEndpoints(context);
    });
}));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Urls.Add("http://*:80");

app.Run();
