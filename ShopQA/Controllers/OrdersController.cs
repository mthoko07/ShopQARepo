
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopQA.Data;
using ShopQA.Models;

namespace ShopQA.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ShopDbContext _db;

    public OrdersController(ShopDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetOrders()
    {
        var orders = await _db.Orders
            .Include(order => order.Items)
            .ToListAsync();

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderById(int id)
    {
        var order = await _db.Orders
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(
        CreateOrderRequest request)
    {
        if (request.Items == null || request.Items.Count == 0)
        {
            return BadRequest("An order must contain at least one item.");
        }

        // Combine duplicate product IDs into one quantity.
        var requestedItems = request.Items
            .GroupBy(item => item.ProductId)
            .Select(group => new
            {
                ProductId = group.Key,
                Quantity = group.Sum(item => item.Quantity)
            })
            .ToList();

        var products = new List<Product>();

        foreach (var requested in requestedItems)
        {
            var product = await _db.Products
                .FirstOrDefaultAsync(p => p.Id == requested.ProductId);

            if (product == null || !product.IsActive)
            {
                return BadRequest(
                    $"Product {requested.ProductId} does not exist or is inactive.");
            }

            if (requested.Quantity < 1 ||
                requested.Quantity > product.Stock)
            {
                return BadRequest(
                    $"Insufficient stock for {product.Name}.");
            }

            products.Add(product);
        }

        // Calculate prices using current database values.
        decimal subtotal = 0m;
        var order = new Order
        {
            Status = "Confirmed",
            OrderDate = DateTime.UtcNow
        };

        foreach (var requested in requestedItems)
        {
            var product = products.First(
                p => p.Id == requested.ProductId);

            subtotal += product.Price * requested.Quantity;

            order.Items.Add(new OrderItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = requested.Quantity,
                UnitPrice = product.Price
            });

            product.Stock -= requested.Quantity;
        }

        order.Subtotal = subtotal;
        order.Vat = Math.Round(subtotal * 0.15m, 2);
        order.Shipping = subtotal >= 500m ? 0m : 60m;
        order.Total = order.Subtotal + order.Vat + order.Shipping;

        _db.Orders.Add(order);

        await using var transaction =
            await _db.Database.BeginTransactionAsync();

        try
        {
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return CreatedAtAction(
            nameof(GetOrders),
            new { id = order.Id },
            order);
    }
}
