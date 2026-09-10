using Microsoft.EntityFrameworkCore;

namespace CinemaApp;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();

    builder.Services.AddScoped<IFileUpload, FileUpload>();

    builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
    builder.Services.AddScoped<IRepository<Cinema>, Repository<Cinema>>();
    builder.Services.AddScoped<IRepository<Actor>, Repository<Actor>>();
    builder.Services.AddScoped<IRepository<Movie>, Repository<Movie>>();
    builder.Services.AddScoped<IBulkRepository<MovieSubImg>, BulkRepository<MovieSubImg>>();
    builder.Services.AddScoped<IBulkRepository<MovieActor>, BulkRepository<MovieActor>>();

    var connectionString =
                    builder.Configuration.GetConnectionString("DefaultConnection")
                        ?? throw new InvalidOperationException("Connection string"
                        + "'DefaultConnection' not found.");

    builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
    {
      optionsBuilder.UseSqlServer(connectionString);
    });

    var app = builder.Build();


    var defaultCulture = new System.Globalization.CultureInfo("en-US");
    System.Globalization.CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
    System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;
    app.UseRequestLocalization(new Microsoft.AspNetCore.Builder.RequestLocalizationOptions
    {
      DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture),
      SupportedCultures = new[] { defaultCulture },
      SupportedUICultures = new[] { defaultCulture },
    });

    if (!app.Environment.IsDevelopment())
    {
      app.UseExceptionHandler("/Home/Error");
      app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

    app.Run();
  }
}
