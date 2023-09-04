using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineEvent.Abstract.Repositories;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Abstract.UnitOfWorks;
using OnlineEvent.API.Middlewares;
using OnlineEvent.Core.Configurations;
using OnlineEvent.Core.Entities;
using OnlineEvent.Data;
using OnlineEvent.Data.Repositories;
using OnlineEvent.Data.UnitOfWorks;
using OnlineEvent.Model;
using OnlineEvent.Service.Mapping;
using OnlineEvent.Service.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<CustomTokenOption>(opt => builder.Configuration.GetSection("TokenOption").Bind(opt));
var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
builder.Services.Configure<List<Client>>(opt => builder.Configuration.GetSection("Clients").Bind(opt));
builder.Services.Configure<ApiBehaviorOptions>(x =>
{
    x.SuppressModelStateInvalidFilter = true;
});

// Add services to the container.


builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ICategoryService,CategoryService>();

builder.Services.AddScoped<ICityRepository,CityRepository>();
builder.Services.AddScoped<ICityService,CityService>();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddScoped<IAuthenticationRepository,AuthenticationRepository>();
builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();
builder.Services.AddScoped<ITokenService,TokenService>();


builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), opt =>
    {
        opt.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric= true;
    opt.User.RequireUniqueEmail = true;
    opt.Lockout.MaxFailedAccessAttempts = 5;
    opt.Lockout.AllowedForNewUsers = false;
    opt.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders();

//builder.Services.ConfigureApplicationCookie(opt =>
//{
//    opt.Cookie.Name = "member_cookie";
//    opt.Cookie.HttpOnly = true;
//    opt.ExpireTimeSpan = TimeSpan.FromMinutes(90);
//    opt.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
//    opt.SlidingExpiration = true;
//});

// Kimlik doðrulama mekanizmasý
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        // Gelen token kontrol edilmesi
        ValidIssuer= tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
        ValidateIssuerSigningKey = true,
        ValidateAudience=true,
        ValidateIssuer=true,
        ValidateLifetime=true
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCustomException();
app.UseAuthorization();
app.MapControllers();
app.Run();
