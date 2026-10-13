
using System.ComponentModel.DataAnnotations;

namespace ShopQA.Models;

public class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Confirmed";

    public decimal Subtotal { get; set; }

    public decimal Vat { get; set; }

    public decimal Shipping { get; set; }

    public decimal Total { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}
