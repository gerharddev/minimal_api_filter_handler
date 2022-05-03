// ----------------------------------------------------------------------------------
// Information:
// https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-7-preview-3/
// ----------------------------------------------------------------------------------

using MinimalApiFilters.Filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

string GreatName(string name) => $"Welcome to my API, {name}!";
//Adding a filter to the route
app.MapGet("/v1/great/{name}", GreatName)
    .AddFilter(async (context, next) =>
    {
        var name = (string)context.Parameters[0];
        if (name == "Bill")
        {
            return Results.Problem("No Bill is allowed to my API");
        }
        return await next(context);
    });

//Adding a customer filter to the route. Implement IRouteHandlerFilter
app.MapGet("/v2/great/{name}", GreatName)
    .AddFilter<NameValidator>();

app.Run();

