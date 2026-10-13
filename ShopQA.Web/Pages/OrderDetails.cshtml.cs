
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopQA.Web.Models;

namespace ShopQA.Web.Pages;

public class OrderDetailsModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrderDetailsModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public OrderViewModel? Order { get; set; }

    public string? Message { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("ShopQAApi");

        try
        {
            var response = await client.GetAsync($"/api/orders/{id}");

            if (response.StatusCode ==
                System.Net.HttpStatusCode.NotFound)
            {
                Message = $"Order {id} was not found.";
                return Page();
            }

            response.EnsureSuccessStatusCode();

            Order = await response.Content
                .ReadFromJsonAsync<OrderViewModel>();

            if (Order == null)
            {
                Message = "Unable to load the order details.";
            }
        }
        catch (HttpRequestException)
        {
            Message = "Unable to connect to the ShopQA API.";
        }

        return Page();
    }
}
