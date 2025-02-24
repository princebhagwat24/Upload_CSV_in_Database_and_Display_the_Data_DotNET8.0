//using CsvToDatabaseApi.Data;
//using CsvToDatabaseApi.Services;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();  // Enable Swagger UI
//builder.Services.AddDbContext<DatabaseContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Set up database connection
//builder.Services.AddScoped<CsvService>();
//builder.Services.AddScoped<TableCreatorService>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseAuthorization();

//app.MapControllers();

//app.Run();



using CsvToDatabaseApi.Data;
using CsvToDatabaseApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Enable Swagger UI

// Set up database connection with SQL Server using connection string from appsettings.json
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register scoped services for CsvService and TableCreatorService
builder.Services.AddScoped<CsvService>();    // Csv parsing service
builder.Services.AddScoped<TableCreatorService>(); // Table data insert service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// Map controllers to routes
app.MapControllers();

// Run the app
app.Run();
