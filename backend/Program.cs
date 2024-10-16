using backend.Services;
using backend.Services.AbstractClass;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<BedService>();
builder.Services.AddScoped<BathRoomServices>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<HotelService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<RoomFiltersService>();
builder.Services.AddScoped<RoomInfoServices>();
builder.Services.AddScoped<RoomTemplateService>();
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PriceService>();
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
