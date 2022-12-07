// using Newtonsoft.Json.Serialization;
// using WebApi.Helpers;
// using WebApi.Services;
//
// var builder = WebApplication.CreateBuilder(args);
//
// builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// // Add services to the container.
//
// builder.Services.AddControllers().AddNewtonsoftJson(o =>
// {
//     o.SerializerSettings.ContractResolver = new DefaultContractResolver();
// });
//
// builder.Services.AddScoped<IUserService, UserService>();
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     
// }
//
// app.UseHttpsRedirection();
//
// app.UseAuthorization();
//
// app.MapControllers();
//
// app.Run();

using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers().AddNewtonsoftJson(o =>
    {
        o.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run("http://localhost:4000");