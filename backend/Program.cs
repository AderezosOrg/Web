using backend.Services;
using backend.Services.AbstractClass;
//
using Db;
using MySql.Data;
using MySql.Data.MySqlClient;


MySqlConnection conn;
string myConnectionString;

myConnectionString = "server=localhost;port=3307;uid=root;" +
"pwd=DFAE824F33EA9ED3BB445CFA5C1BFA305F47BF4A;database=dbaderezosweb;Allow User Variables=True";

       
conn = new MySqlConnection(myConnectionString);


conn.Open();

        
IDataInjector injector = new BathrommDataInjector();
int result = injector.InjectData(conn);
Console.WriteLine(result);

injector = new BedDataInjector();
result = injector.InjectData(conn);
Console.WriteLine(result);

injector = new ContactDataInjector();
result = injector.InjectData(conn);
Console.WriteLine(result);

injector = new ServiceDataInjector();
result = injector.InjectData(conn);
Console.WriteLine(result);

injector = new RoomTemplateDataInjector();
result = injector.InjectData(conn);
Console.WriteLine(result);

injector = new UserDataInjector();
result = injector.InjectData(conn);
Console.WriteLine(result);

injector = new HotelDataInjector();
result = injector.InjectData(conn);
Console.WriteLine(result);

injector = new RoomDataInjector();
result = injector.InjectData(conn);
Console.WriteLine(result);

injector = new RoomServicesDataInjector();
result = injector.InjectData(conn);
Console.WriteLine(result);

injector = new ReservationDataInjector();
result = injector.InjectData(conn);
Console.WriteLine(result);

injector = new BedInformationDataInjector();
result = injector.InjectData(conn);
Console.WriteLine(result);

injector = new RoombathDataInjector();
result = injector.InjectData(conn);
Console.WriteLine(result);
//

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
