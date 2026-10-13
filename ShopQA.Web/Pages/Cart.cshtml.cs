
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopQA.Web.Models;
using System.Text.Json;

namespace ShopQA.Web.Pages;

public class CartModel : PageModel
{
    public List<CartItemViewModel> Items { get; set; } = new();

    public decimal Total => Items.Sum(item => item.Subtotal);

    public void OnGet()
    {
        var cartJson = HttpContext.Session.GetString("Cart");

        if (!string.IsNullOrEmpty(cartJson))
        {
            Items = JsonSerializer.Deserialize<List<CartItemViewModel>>(
                cartJson) ?? new List<CartItemViewModel>();
        }
    }

    public IActionResult OnPostRemove(int productId)
    {
        var cartJson = HttpContext.Session.GetString("Cart");

        if (!string.IsNullOrEmpty(cartJson))
        {
            var items =
                JsonSerializer.Deserialize<List<CartItemViewModel>>(cartJson)
                ?? new List<CartItemViewModel>();

            items.RemoveAll(item => item.ProductId == productId);

            HttpContext.Session.SetString(
                "Cart",
                JsonSerializer.Serialize(items));
        }

        return RedirectToPage();
    }

}
