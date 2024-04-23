using IdentityModel.Client;
using MemberClient.APIServices;
using MemberClient.HttpHandlers;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMemberService, MemberService>();

//MemberAPI HttpClient
builder.Services.AddTransient<AutenticationDelegatingHandler>();
builder.Services.AddHttpClient("MemberClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7057/");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddHttpMessageHandler<AutenticationDelegatingHandler>();

//IdentityServer HttpClient
builder.Services.AddHttpClient("IdentityServer", client =>
{
    client.BaseAddress = new Uri("https://localhost:7280/");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

//Token Generation
builder.Services.AddSingleton(new ClientCredentialsTokenRequest
{
    Address = "https://localhost:7280/connect/token",
    ClientId = "memberClient",
    ClientSecret = "secret",
    Scope = "MemberAPI"

});  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
