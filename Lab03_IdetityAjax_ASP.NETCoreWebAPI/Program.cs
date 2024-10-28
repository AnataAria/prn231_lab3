using DataAccessLayer;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductDBContext>();
builder.Services.AddScoped(typeof(BaseRepository<,>));
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowAll");
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProductDBContext>();
    await context.Database.MigrateAsync();
}

app.Run();
