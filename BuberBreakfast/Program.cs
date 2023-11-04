using BuberBreakfast.Models;
using BuberBreakfast.Services;
using BuberBreakfast.Services.Breakfasts;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers();
    // This is important to tell controller how to instantiate a Breakfast Service
    // This line: every time we instantiate an object, it requests the Ibreakfast service
    // in the constructor, then use BrekafastService obj as its implementation
    // also see request lifetime: AddScoped, AddTransient
    builder.Services.AddScoped<IBreakfastService, BreakfastService>();
}
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
var app = builder.Build();
/* Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
{
    app.UseExceptionHandler("/error");
    // /error route is defined in ErrorsController
    app.UseHttpsRedirection();
    // app.UseAuthorization();
    app.MapControllers();
    app.Run();
}