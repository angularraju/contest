using SimplifiedS3Bucket.Services.Implementations;
using SimplifiedS3Bucket.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var filePath = Environment.GetEnvironmentVariable("FILE_PATH");//@"C:\New";
Console.WriteLine($"FilePath is {filePath}");
if (string.IsNullOrEmpty(filePath))
    throw new Exception("FILE_PATH environment variable is not set");

builder.Services.AddSingleton(filePath);
builder.Services.AddSingleton<IFileRepository, FileRepository>();
builder.Services.AddTransient<IFileHandler, FileHandler>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
