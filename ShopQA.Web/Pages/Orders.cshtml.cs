
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopQA.Web.Models;

namespace ShopQA.Web.Pages;

public class OrdersModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrdersModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<OrderViewModel> Orders { get; set; } = new();

    public string? Message { get; set; }

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("ShopQAApi");

        try
        {
            Orders = await client.GetFromJsonAsync<List<OrderViewModel>>(
                "/api/orders") ?? new List<OrderViewModel>();
        }
        catch (HttpRequestException)
        {
            Message = "Unable to connect to the ShopQA API. Please try again.";
        }
    }
}
