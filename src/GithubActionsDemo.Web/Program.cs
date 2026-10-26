var builder = WebApplication.CreateBuilder(args);

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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapGet("/version", (IConfiguration configuration) =>
{
    return Results.Ok(new
    {
        Version = configuration["APP_VERSION"] ?? "local",
        Environment = app.Environment.EnvironmentName
    });
});

app.MapGet("/api/calculate-discount", (decimal price, decimal discount) =>
{
    var discountedPrice = price - (price * discount / 100);
    return Results.Ok(new { discountedPrice });
});

app.Run();
