using Adinux.Wasm.Server;
using Adinux.Wasm.Server.BackgroundServices.Queues;
using Adinux.Wasm.Server.Models;
using Adinux.Wasm.Server.Repository;
using Adinux.Wasm.Server.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Tailwind;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddOptions();
builder.Services.AddDbContext<EFContext>();
builder.Services.AddHttpClient();
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));

builder.Services.AddScoped<TelegramLoggerService>();
builder.Services.AddScoped<DataRepository>();
builder.Services.AddSingleton<MailSenderService>();
builder.Services.AddSingleton<CatalogueRequestSenderQueue>();
builder.Services.AddSingleton<IHostedService, CatalogueRequestSenderDequeuer>();
//builder.Services.AddSingleton<WTelegramService>();
//builder.Services.AddHostedService(provider => provider.GetService<WTelegramService>());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.RunTailwind("tailwind", "../Client/");

}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
