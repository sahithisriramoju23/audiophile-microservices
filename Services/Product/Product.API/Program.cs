using Product.API;
using Product.API.Data.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Move AddAWSLambdaHosting to builder.Host, and use correct syntax
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.InitialiseDatabaseAsync();
}

app.UseHttpsRedirection();

app.UseApplicationServices();

app.MapGet("/hearbeat", () =>
{
    return Results.Ok("Products Service is Up and Running");
})
.WithName("Heartbeat")
.WithOpenApi();

app.Run();
