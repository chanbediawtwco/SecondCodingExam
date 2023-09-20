using Microsoft.EntityFrameworkCore;
using SecondCodingExam.Data;
using SecondCodingExam;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Enable Swagger
builder.Services.AddSwaggerGen();

// Initialize CORS
// This fix CORS Error
builder.Services.AddCors(options =>
    options.AddPolicy(name: Constants.CorsOrigin,
        policy =>
        {
            // TODO put this in appsettings
            policy.WithOrigins("https://localhost:42200").AllowAnyMethod().AllowAnyHeader();
        }
    )
);

// Database Connection
builder.Services.AddDbContext<SecondCodingExamDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString(Constants.DefaultConnectionString))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // Run swagger service if the environment is a development environment
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS Error Fix
app.UseCors(Constants.CorsOrigin);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
