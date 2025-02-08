namespace BackgroundEmailSenderSample;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddSingleton<EmailSenderHostedService>();
        builder.Services.AddSingleton<IHostedService>(serviceProvider => serviceProvider.GetService<EmailSenderHostedService>());
        builder.Services.AddSingleton<IEmailSender>(serviceProvider => serviceProvider.GetService<EmailSenderHostedService>());

        builder.Services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();

        builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection("ConnectionStrings"));
        builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
        await app.RunAsync();
    }
}