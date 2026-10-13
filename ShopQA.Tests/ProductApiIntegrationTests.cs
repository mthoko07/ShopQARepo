using System.Net;
using System.Net.Http.Json;
using ShopQA.Models;
using Xunit;

namespace ShopQA.Tests;

public class ProductApiIntegrationTests : IClassFixture<ShopQAFactory>
{
    private readonly HttpClient _client;

    public ProductApiIntegrationTests(ShopQAFactory factory)
    {
        _client = factory.CreateClient();
    }

    private static Product NewProduct(string name) => new()
    {
        Name = name,
        Description = "Created by an integration test",
        Price = 199.99m,
        Stock = 10,
        CategoryId = 3,
        IsActive = true
    };

    [Fact]
    public async Task GetProducts_ReturnsSuccessStatusCodeAndProductList()
    {
        var response = await _client.GetAsync("/api/products");
        var body = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
        Assert.StartsWith("[", body.Trim());
    }

    [Fact]
    public async Task GetProduct_WithNonExistingId_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/products/99999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateProduct_WithValidData_ReturnsCreated()
    {
        var response = await _client.PostAsJsonAsync(
            "/api/products", NewProduct("Integration Test Mouse"));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProduct_WithExistingId_ReturnsNoContent()
    {
        var createResponse = await _client.PostAsJsonAsync(
            "/api/products", NewProduct("Product To Delete"));
        var created = await createResponse.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(created);

        var deleteResponse = await _client.DeleteAsync($"/api/products/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getResponse = await _client.GetAsync($"/api/products/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task UpdateProduct_WithValidData_ReturnsOk()
    {
        var createResponse = await _client.PostAsJsonAsync(
            "/api/products", NewProduct("Mouse Before Update"));
        var created = await createResponse.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(created);

        created.Name = "Mouse After Update";
        created.Price = 249.99m;

        var updateResponse = await _client.PutAsJsonAsync(
            $"/api/products/{created.Id}", created);
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        var getResponse = await _client.GetAsync($"/api/products/{created.Id}");
        var updated = await getResponse.Content.ReadFromJsonAsync<Product>();

        Assert.NotNull(updated);
        Assert.Equal("Mouse After Update", updated.Name);
        Assert.Equal(249.99m, updated.Price);
    }
}