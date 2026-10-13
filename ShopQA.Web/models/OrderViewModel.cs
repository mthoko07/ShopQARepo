
namespace ShopQA.Web.Models;

public class OrderViewModel
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public decimal Subtotal { get; set; }

    public decimal Vat { get; set; }

    public decimal Shipping { get; set; }

    public decimal Total { get; set; }

    public List<OrderItemViewModel> Items { get; set; } = new();
}

public class OrderItemViewModel
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Subtotal { get; set; }
}
