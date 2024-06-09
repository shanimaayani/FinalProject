using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project.BLL;
using Project.DAL;
using Project.Middleware;
using Swashbuckle.AspNetCore;
using System.Text;
using webapi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ChineesOctionContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ChineesOctionContext")));
builder.Services.AddScoped<IUserDal, UserDal>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPresentDal, PresentDal>();
builder.Services.AddScoped<IPresentService, PresentService>();
builder.Services.AddScoped<IDonorDal, DonorDal>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<ICartItemDal, CartItemDal>();
builder.Services.AddScoped<ICartItemsService, CartItemsService>();
builder.Services.AddScoped<ICartDal, CartDal>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ILotteryDal, LotteryDal>();
builder.Services.AddScoped<ILotteryService, LotteryService>();
// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>//לאיזה דומיין - פורט יאפשר לגשת
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200",
                "development web site").AllowAnyHeader().AllowAnyMethod();
        });
});


//});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Issuer"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//        };
//    });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseCors("CorsPolicy");
//app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/Lottery"), appBuilder =>
//{
//    appBuilder.UseMiddleware<AuthenticationMiddleware>();
//});
//app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/Cart"), appBuilder =>
//{
//    appBuilder.UseMiddleware<AuthenticationMiddleware>();
//});



IConfiguration configuration = app.Configuration;
IWebHostEnvironment environment = app.Environment;

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
