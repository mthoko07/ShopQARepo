
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopQA.Web.Models;

namespace ShopQA.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<ProductViewModel> Products { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ShopQAApi");

            var products = await client
                .GetFromJsonAsync<List<ProductViewModel>>("/api/products");

            Products = products?
                .Where(product => product.IsActive)
                .ToList() ?? new List<ProductViewModel>();
        }
        catch (HttpRequestException)
        {
            ErrorMessage =
                "Unable to load products. Please make sure the ShopQA API is running.";
        }
    }

    public async Task<IActionResult> OnPostAddToCartAsync(int productId)
    {
        var client = _httpClientFactory.CreateClient("ShopQAApi");

        var product = await client.GetFromJsonAsync<ProductViewModel>(
            $"/api/products/{productId}");

        if (product == null || !product.IsActive || product.Stock <= 0)
        {
            return RedirectToPage();
        }

        var cartJson = HttpContext.Session.GetString("Cart");

        var cart = string.IsNullOrEmpty(cartJson)
            ? new List<CartItemViewModel>()
            : JsonSerializer.Deserialize<List<CartItemViewModel>>(cartJson)
                ?? new List<CartItemViewModel>();

        var existingItem = cart.FirstOrDefault(
            item => item.ProductId == product.Id);

        if (existingItem == null)
        {
            cart.Add(new CartItemViewModel
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = 1
            });
        }
        else if (existingItem.Quantity < product.Stock)
        {
            existingItem.Quantity++;
        }

        HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));

        return RedirectToPage("/Cart");
    }
}
