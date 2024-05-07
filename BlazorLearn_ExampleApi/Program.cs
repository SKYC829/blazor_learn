using BlazorLearn_ExampleApi.Databases;
using BlazorLearn_ExampleApi.Databases.Models;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<AuthExampleContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthExampleDb"));
});

SystemUser us = new SystemUser()
{
    Id = 1,
    Name = "Moc”√ªß",
    Email = "abc@gmail.com",
    SystemUserRoles = new List<SystemUserRole>()
    {
        new SystemUserRole()
        {
            Id = 1,
            Role = new SystemRole()
            {
                Id = 1,
                Code = "Admin",
                Name = "Admin"
            },
            UserId = 1
        }
    },
    SystemUserSecret = new SystemUserSecret()
    {
        Id = 1,
        Password = "123456",
        UserId = 1
    }
};

builder.Services.AddSingleton<SystemUser>(us);

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
