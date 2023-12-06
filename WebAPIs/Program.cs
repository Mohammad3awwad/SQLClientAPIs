using Serilog;
using WebAPIs.ClassesForDI;
using WebAPIs.InterFacesForDI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConnectSQL, ConnectSQL>();

//The below 2 lines used to custom log inside a folder as the below path...
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/Logfile.txt", rollingInterval: RollingInterval.Infinite).CreateLogger();
builder.Host.UseSerilog();

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

app.Run();
