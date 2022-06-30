using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RadioFETestApp.Data;
using RadioFETestApp.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<ApiConnector>();
builder.Services.AddHttpClient();

var radioConnectionString = builder.Configuration.GetConnectionString("RadioFEApi");

builder.Services.AddSingleton(new ConnectionStrings { RadioFEApi = radioConnectionString });

var app = builder.Build();

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
