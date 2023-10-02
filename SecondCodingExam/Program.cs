using Microsoft.EntityFrameworkCore;
using SecondCodingExam.Data;
using SecondCodingExam;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using SecondCodingExam.Mapper;
using SecondCodingExam.Dto;
using SecondCodingExam.Validators;
using FluentValidation;
using SecondCodingExam.Services.Interface;
using SecondCodingExam.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

//JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration[Constants.JwtIssuer],
        ValidAudience = builder.Configuration[Constants.JwtAudience],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[Constants.JwtKey]))
    };
});

// Enable Authorization
builder.Services.AddAuthorization();

// Add mapper
var MapConfiguration = new MapperConfiguration(Configuration =>
{
    Configuration.AddProfile(new MappingProfile());
});
IMapper Mapper = MapConfiguration.CreateMapper();
// Add as singleton to use accross classes
builder.Services.AddSingleton(Mapper);

// Add fluent validators
builder.Services.AddScoped<IValidator<BenefitDto>, BenefitValidator>();
builder.Services.AddScoped<IValidator<CustomerDto>, CustomerValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
builder.Services.AddScoped<IValidator<UserRegistrationDto>, UserRegistrationValidator>();

// Add dependency injection
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAuditService, AuditService>();
builder.Services.AddTransient<IBenefitService, BenefitService>();
builder.Services.AddTransient<ICalculationService, CalculationService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IPaginationService, PaginationService>();
builder.Services.AddTransient<IBenefitService, BenefitService>();
builder.Services.AddTransient<ICustomerHistoryService, CustomerHistoryService>();
builder.Services.AddTransient<IBenefitHistoryService, BenefitHistoryService>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
