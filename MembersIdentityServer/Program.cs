using MembersIdentityServer;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddIdentityServer()
//    .AddInMemoryClients(new List<Client>())
//    .AddInMemoryIdentityResources(new List<IdentityResource>())
//    .AddInMemoryApiResources(new List<ApiResource>())
//    .AddInMemoryApiScopes(new List<ApiScope>())
//    .AddTestUsers(new List<TestUser>())
//    .AddDeveloperSigningCredential();

//builder.Services.AddIdentityServer()
//    .AddInMemoryClients(Config.Clients)
//    .AddInMemoryIdentityResources(Config.IdentityResources)
//    .AddInMemoryApiResources(Config.ApiResources)
//    .AddInMemoryApiScopes(Config.ApiScopes)
//    .AddTestUsers(Config.TestUsers)
//    .AddDeveloperSigningCredential();

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddDeveloperSigningCredential();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseIdentityServer();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
