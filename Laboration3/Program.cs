var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//L�gger till en rad f�r att sessions ska fungera
builder.Services.AddDistributedMemoryCache();

//L�gg till ett block f�r att sessions ska fungera
builder.Services.AddSession(opttions =>
{
    opttions.IdleTimeout = TimeSpan.FromSeconds(100);
    opttions.Cookie.HttpOnly = true;
    opttions.Cookie.IsEssential = true;
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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
