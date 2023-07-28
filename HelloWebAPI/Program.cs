using HelloWebAPI.EmployeeModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
})
.AddXmlSerializerFormatters();

//API Explorer middleware becomes active and starts analyzing the application's routes and controllers.
//It collects information about the available API endpoints, their parameters, HTTP methods, 
//and response types. This information is then made accessible via a structured API description
//that tools like Swagger UI can use to generate human-readable documentation for the API.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<EmployeeContext>(Options=> Options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeContext")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//Automatically redirects Http to Https even if user enters as Http in url
app.UseHttpsRedirection();

//Adds endpoints for controller actions to the IEndpointRouteBuilder without specifying any routes
app.MapControllers();


//When you want to use routing explicity use below two lines 
//app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/", async context =>
//    {
//        await context.Response.WriteAsync("Hello World!");
//    });

//    app.UseEndpoints(endpoints =>
//    {
//        endpoints.MapControllers();
//    });
//});

app.Run();

