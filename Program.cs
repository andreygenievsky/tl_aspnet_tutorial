using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoRestApi.Models;
using TodoRestApi.Data;
using TodoRestApi.Utils;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

//!! Unsuccessful attempts to fix server security protocol issue
// using System;
// using System.Net;
// Console.WriteLine(ServicePointManager.SecurityProtocol);
// ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
// Console.WriteLine(ServicePointManager.SecurityProtocol);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("*")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
	    options.SerializerSettings.ContractResolver = new PatchRequestContractResolver();
	});

builder.Services.AddDbContext<PersistenceContext>(opt => opt.UseInMemoryDatabase("ToDoDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//!!! Something wrong with server TLS version in my local machine
//app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseCors("AllowPATCH");

app.UseAuthorization();
app.MapControllers();

app.Run();
