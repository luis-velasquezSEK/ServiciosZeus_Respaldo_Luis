using AspNetCoreRateLimit;
using Serilog;
using Servicios_Zeus.Extensions;
using Servicios_Zeus.Helpers.Errors;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var logger=new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
//builder.Logging.ClearProviders();//descomentar en produccion para omitir codigos de error
builder.Services.ConfigureRateLimitiong();
builder.Logging.AddSerilog(logger);
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.AddAplicationService();
builder.Services.AddControllers();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);

builder.Services.AddValidationErrors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseIpRateLimiting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
