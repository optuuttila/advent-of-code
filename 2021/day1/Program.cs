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

app.MapGet("/result/{depth}", (int depth) =>
{
    var depths = GetDepthMeasurements();
    var result = CountDepthIncrements(depths, depth);
    return result;

})
.WithName("result");
app.MapGet("/", (LinkGenerator linker) => 
        $"Advent of code Day 1." +
        //TODO: Make these direct links to page
        $"\nResult 1 url: {linker.GetPathByName("result", values: new {depth = 1})}" + 
        $"\nResult 2 url: {linker.GetPathByName("result", values: new {depth = 3})}" +
        //TODO: Wonder how the swagger url can be retrieved with the linker?
        $"\nSwagger url: /swagger"
        );

app.Run();

List<int> GetDepthMeasurements() {
    var input = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "input.txt");
    var depths = System.IO.File.ReadAllLines(input).Select(int.Parse).ToList();
    return depths;
}

int CountDepthIncrements(List<int> depths, int howDeepToGo) {
    var count = 0;
    for(int i=0; i < depths.Count-howDeepToGo; i++) {
        var sumOfDepthFirst = depths.GetRange(i, howDeepToGo).Sum();
        var sumOfDepthSecond = depths.GetRange(i+1, howDeepToGo).Sum();
        if (sumOfDepthSecond > sumOfDepthFirst) {
            count++;
        }
    }
    return count;
}