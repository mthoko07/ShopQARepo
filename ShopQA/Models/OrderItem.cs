
using System.ComponentModel.DataAnnotations;

namespace ShopQA.Models;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    [Required]
    [StringLength(100)]
    public string ProductName { get; set; } = string.Empty;

    [Range(1, 100000)]
    public int Quantity { get; set; }

    [Range(typeof(decimal), "0.01", "1000000")]
    public decimal UnitPrice { get; set; }

    public decimal Subtotal => UnitPrice * Quantity;

    public Order? Order { get; set; }
}
