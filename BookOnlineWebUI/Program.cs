using BookMS.Infrastrucure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();


#region BookManagementSystem Services
BookMSDI.RegisterServices(builder.Services,builder.Configuration.GetConnectionString("defaultConnection"));
#endregion

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();
