using BookMS.Infrastrucure;
using Serilog;
using Serilog.Events;

var logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

try
{

    var builder = WebApplication.CreateBuilder();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);

    builder.Services.AddRazorPages();
    builder.Services.AddSingleton<Serilog.ILogger>(x => logger);


    #region BookManagementSystem Services
    BookMSDI.RegisterServices(builder.Services, builder.Configuration.GetConnectionString("defaultConnection"));
    #endregion

    var app = builder.Build();

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    app.MapRazorPages();

    logger.Information("Application is running");
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex.Message);
}
finally
{
    logger?.Dispose();
}
