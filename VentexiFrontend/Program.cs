var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();




var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Invoice}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
