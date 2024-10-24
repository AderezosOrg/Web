using backend.Services;
using Db;

DbUtils.OpenConnection();

// Delete to conserve persistency.
DbUtils.TruncateAllTables();

DbUtils.InjectData();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.Scan(scan => scan
    .FromAssemblyOf<RoomService>()
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")  && type.Namespace == "backend.Services"))
    .AsImplementedInterfaces()
    .WithScopedLifetime());
builder.Services.AddControllers();

builder.Services.Scan(scan => scan
    .FromAssemblyOf<RoomDAO>()
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("DAO")  && type.Namespace == "Db"))
    .AsSelf()
    .AsImplementedInterfaces()
    .WithScopedLifetime());
builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder.WithOrigins("http://localhost:5173", "http://localhost:5174")
            .AllowAnyMethod()
            .AllowAnyHeader()
        );
    
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
