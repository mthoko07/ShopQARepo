
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopQA.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace ShopQA.Web.Pages;

public class CheckoutModel : PageModel
{
    public List<CartItemViewModel> Items { get; set; } = new();

    public decimal Subtotal => Items.Sum(item => item.Subtotal);

    public decimal Vat => Math.Round(Subtotal * 0.15m, 2);

    public decimal Shipping => Subtotal >= 500m ? 0m : 60m;

    public decimal Total => Subtotal + Vat + Shipping;
    public string? Message { get; set; }

    public bool OrderSucceeded { get; set; }
    public void OnGet()
    {
        var cartJson = HttpContext.Session.GetString("Cart");

        if (!string.IsNullOrEmpty(cartJson))
        {
            Items = JsonSerializer.Deserialize<List<CartItemViewModel>>(cartJson)
                ?? new List<CartItemViewModel>();
        }
    }
    public async Task<IActionResult> OnPostPlaceOrderAsync()
    {
        var cartJson = HttpContext.Session.GetString("Cart");

        if (string.IsNullOrEmpty(cartJson))
        {
            Message = "Your cart is empty.";
            return Page();
        }

        var cart = JsonSerializer.Deserialize<List<CartItemViewModel>>(cartJson)
            ?? new List<CartItemViewModel>();

        if (cart.Count == 0)
        {
            Message = "Your cart is empty.";
            return Page();
        }

        var request = new
        {
            Items = cart.Select(item => new
            {
                item.ProductId,
                item.Quantity
            }).ToList()
        };

        try
        {
            var client = HttpContext.RequestServices
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient("ShopQAApi");

            var response = await client.PostAsJsonAsync(
                "/api/orders", request);

            if (!response.IsSuccessStatusCode)
            {
                Message = "We couldn't place your order. Please check product availability and try again.";
                return Page();
            }

            HttpContext.Session.Remove("Cart");
            OrderSucceeded = true;
            Message = "Your order was placed successfully!";
            Items.Clear();

            return Page();
        }
        catch (HttpRequestException)
        {
            Message = "Unable to contact the ShopQA API. Please try again.";
            return Page();
        }
    }
}
