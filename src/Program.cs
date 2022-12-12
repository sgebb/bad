using Bad.Database;
using Bad.Numbers;
using Bad.Strings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<INumbersDataAccess, NumbersDataAccess>();
builder.Services.AddTransient<IStringsDataAccess, StringsDataAccess>();
builder.Services.AddTransient<IStringsDomain, StringsDomain>();

builder.Services.AddDbContext<BadDbContext>(opt =>
{
    opt.UseSqlServer(DbHelper.DbConnectionString(builder.Configuration));
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// make sure db is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BadDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
