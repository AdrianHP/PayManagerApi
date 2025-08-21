using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayManager.Bootstrapper;
using PayManager.Business.Contracts.Service;
using PayManager.Business.Implementation.Service;
using PayManager.DataAccess.Contracts;
using PayManager.DataAccess.Implementation.DBContext;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;
var services = builder.Services;


builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

//Mapping Config
services.AddAutoMapper(typeof(Program));
var mapperConfiguration = new MapperConfiguration(_ => { });
var mapper = mapperConfiguration.CreateMapper();
services.AddSingleton(mapper);



services
    .AddDbContext<PayManagerContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    })
    .AddTransient<IObjectContext, PayManagerContext>();


services.BootstrapServices(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
