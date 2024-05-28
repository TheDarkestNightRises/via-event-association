using ViaEventAssociation.Core.Application.Extensions;
using ViaEventAssociation.Core.QueryContracts.Extensions;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Extensions;
using ViaEventAssociation.Infrastructure.EfcQueries.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterHandlers();
builder.Services.RegisterCommandDispatcher();
builder.Services.RegisterQueryDispatcher();
builder.Services.RegisterRepository();
builder.Services.RegisterQueryHandlers();
builder.Services.RegisterMapper();
builder.Services.AddControllers();


var app = builder.Build();

app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();