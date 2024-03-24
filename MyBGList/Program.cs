using MyBGList.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseDeveloperExceptionPage();
}

if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
    app.UseDeveloperExceptionPage(); 
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Error
app.MapGet("/error", () => Results.Problem());
app.MapGet("/error/test", () => { throw new Exception("test"); });

app.MapGet("/person", () => new Person("Andrew", "Lock"));

List<Person> people = new()
    {
        new("Tom", "Hanks"),
        new("Denzel", "Washington"),
        new("Leondardo", "DiCaprio"),
        new("Al", "Pacino"),
        new("Morgan", "Freeman"),
    };

app.MapGet("/person/{name}", (string name) => people.Where(p => p.FirstName.StartsWith(name)));

// BoardGame
app.MapGet("/boardgames", () => new BoardGame[] {
                                                                    new () {
                                                                        Id = 1,
                                                                        Name = "Axis & Allies",
                                                                        Year = 1981
                                                                    },
                                                                    new () {
                                                                        Id = 2,
                                                                        Name = "Citadels",
                                                                        Year = 2000
                                                                    },
                                                                    new () {
                                                                        Id = 3,
                                                                        Name = "Terraforming Mars",
                                                                        Year = 2016
                                                                    }
                                                                }
                );


//app.MapControllers();

app.Run();


public record Person(string FirstName, string LastName);