using TodoListApiSqlite.Data;
using Microsoft.EntityFrameworkCore;
using TodoListApiSqlite.Repositories;
using TodoListApiSqlite.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TodoListApiContext>
    (options => options.UseSqlite("Name=TodoListApiDB"));

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IJWTAuthenticationManager, JWTAuthenticationManager>();

builder.Services.AddSingleton<NoteService>();
builder.Services.AddSingleton<NoteRepository>();

builder.Services.AddSingleton<GroupService>();
builder.Services.AddSingleton<GroupRepository>();

builder.Services.AddSingleton<GroupUserRepository>();

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var key = Encoding.ASCII.GetBytes(config["TokenSecret"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

/*app.UseHttpsRedirection();*/

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
