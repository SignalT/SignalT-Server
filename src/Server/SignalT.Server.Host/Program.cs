using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SignalT.Server.Host.Features.ServerLogs;

namespace SignalT.Server.Host;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSignalR();
        builder.Services.AddHostedService<YourBackgroundService>();
        builder.Services.AddSingleton<IServerLogsService, ServerLogsService>();
        var app = builder.Build();
        app.MapHub<ListHub>("/listhub");

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}