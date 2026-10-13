using Microsoft.EntityFrameworkCore;
using ShopQA.Data;
using ShopQA.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));
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
//app.UseRouting();

app.UseAuthorization();

//app.MapStaticAssets();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ShopDbContext>();

    if (!db.Categories.Any())
    {
        var electronics = new Category { Name = "Electronics" };
        var computers = new Category { Name = "Computers" };
        var accessories = new Category { Name = "Accessories" };

        db.Categories.AddRange(electronics, computers, accessories);
        db.SaveChanges();

        db.Products.AddRange(
            new Product
            {
                Name = "Wireless Mouse",
                Description = "Wireless USB mouse",
                Price = 299.99m,
                Stock = 20,
                CategoryId = accessories.Id,
                IsActive = true
            },
            new Product
            {
                Name = "Keyboard",
                Description = "USB keyboard",
                Price = 499.99m,
                Stock = 15,
                CategoryId = accessories.Id,
                IsActive = true
            },
            new Product
            {
                Name = "Laptop",
                Description = "15-inch laptop",
                Price = 8999.99m,
                Stock = 5,
                CategoryId = computers.Id,
                IsActive = true
            }
        );

        db.SaveChanges();
    }
}

app.Run();
app.Run();
