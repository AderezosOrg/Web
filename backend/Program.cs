using backend.Services;
using backend.Services.AbstractClass;
using Db;
using Entities;

DbUtils.OpenConnection();

// Delete to conserve persistency.
DbUtils.TruncateAllTables();

DbUtils.InjectData();

Guid id = Guid.NewGuid();

// Create
Console.WriteLine("Create");
BathroomTableOperator.Create(new Bathroom{
    BathRoomID = id,
    Shower = true,
    Toilet = true,
    DressingTable = true
});

//Read Created
Console.WriteLine("Read Created");
var b2  = BathroomTableOperator.Read(id);
Console.WriteLine(b2.BathRoomID);
Console.WriteLine(b2.Shower);
Console.WriteLine(b2.Toilet);
Console.WriteLine(b2.DressingTable);


// Update
Console.WriteLine("Update");
int rowsUpdated = BathroomTableOperator.Update(
    new Bathroom{
        BathRoomID = id,
        Shower = false,
        Toilet = false,
        DressingTable = false
    }
);

//Read Updated
Console.WriteLine("Read Updated");
b2  = BathroomTableOperator.Read(id);
Console.WriteLine(b2.BathRoomID);
Console.WriteLine(b2.Shower);
Console.WriteLine(b2.Toilet);
Console.WriteLine(b2.DressingTable);

//Delete
Console.WriteLine("Delete");
BathroomTableOperator.Delete(id);

//Read Deleted?
Console.WriteLine("Read Deleted?");
b2  = BathroomTableOperator.Read(id);
Console.WriteLine(b2 is null);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Scan(scan => scan
    .FromAssemblyOf<RoomService>()
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")  && type.Namespace == "backend.Services"))
    .AsSelf()
    .AsImplementedInterfaces()
    .WithScopedLifetime());
builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder.WithOrigins("http://localhost:5173", "http://localhost:5174")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHsts();
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
        { 
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Work v1"); 
            c.RoutePrefix = string.Empty;
        }
);
    
}

app.UseRouting();
app.UseCors("AllowLocalhost");
app.UseAuthorization();
app.MapControllers();

app.Run();
