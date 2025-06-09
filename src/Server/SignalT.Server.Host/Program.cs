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

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddServer(builder.Configuration);
        var app = builder.Build();
        app.MapHub<ListHub>("/listhub");

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
        app.MapControllers();
        app.Run();
    }
}