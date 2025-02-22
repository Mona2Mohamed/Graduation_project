using Identity;
using Identity.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repo_Core;
using Identity.Service;
using Repo_Core.Interface;
using Repo_EF;
using Repo_EF.Repo_Method;
using System.Text;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeafultConnection"),
    o => o.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

builder.Services.AddDbContext<MyBackupDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecondConnection"),
    o => o.MigrationsAssembly(typeof(MyBackupDbContext).Assembly.FullName)));

builder.Services.AddDbContext<UserDbcontext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeafultConnection"),
    o => o.MigrationsAssembly(typeof(UserDbcontext).Assembly.FullName)));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<UserDbcontext>();

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
//builder.Services.AddTransient(typeof(IRegsiter<>), typeof(Regsiter_Method<>));
builder.Services.AddTransient<IUnitWork, UnitWork>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
