var builder = WebApplication.CreateBuilder(args);

// Add services here BEFORE building app
builder.Services.AddRazorPages();

// Add HttpClient with base address
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5109"); // Your API base URL
});

// ... add other services here ...

var app = builder.Build();

// Configure middleware, endpoints etc
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();
