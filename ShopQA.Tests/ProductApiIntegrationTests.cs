
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ShopQA.Data;
using Xunit;
using System.Net;
using System.Net.Http.Json;
using ShopQA.Models;

namespace ShopQA.Tests;

public class ProductApiIntegrationTests
{

    [Fact]
    public async Task GetProducts_ReturnsSuccessStatusCodeAndProductList()
    {
        // Arrange
        await using var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    using var provider = services.BuildServiceProvider();
                    using var scope = provider.CreateScope();

                    var db = scope.ServiceProvider
                        .GetRequiredService<ShopDbContext>();

                    db.Database.EnsureCreated();
                });
            });

        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/products");

        var responseBody = await response.Content.ReadAsStringAsync();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.False(string.IsNullOrWhiteSpace(responseBody));
        Assert.StartsWith("[", responseBody.Trim());
    }

    [Fact]
    public async Task GetProduct_WithNonExistingId_ReturnsNotFound()
    {
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var response = await client.GetAsync("/api/products/99999");

        Assert.Equal(
            System.Net.HttpStatusCode.NotFound,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateProduct_WithValidData_ReturnsCreated()
    {
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var product = new Product
        {
            Name = "Integration Test Mouse",
            Description = "Created by an integration test",
            Price = 199.99m,
            Stock = 10,
            CategoryId = 3,
            IsActive = true
        };

        var response = await client.PostAsJsonAsync(
            "/api/products", product);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProduct_WithExistingId_ReturnsNoContent()
    {
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Create a product to delete
        var product = new Product
        {
            Name = "Product To Delete",
            Description = "Temporary test product",
            Price = 99.99m,
            Stock = 5,
            CategoryId = 3,
            IsActive = true
        };

        var createResponse = await client.PostAsJsonAsync(
            "/api/products", product);

        var createdProduct = await createResponse.Content
            .ReadFromJsonAsync<Product>();

        Assert.NotNull(createdProduct);

        // Delete the product
        var deleteResponse = await client.DeleteAsync(
            $"/api/products/{createdProduct.Id}");

        Assert.Equal(
            System.Net.HttpStatusCode.NoContent,
            deleteResponse.StatusCode);

        // Confirm the product no longer exists
        var getResponse = await client.GetAsync(
            $"/api/products/{createdProduct.Id}");

        Assert.Equal(
            System.Net.HttpStatusCode.NotFound,
            getResponse.StatusCode);
    }


    [Fact]
    public async Task UpdateProduct_WithValidData_ReturnsOk()
    {
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Create a product first
        var product = new Product
        {
            Name = "Mouse Before Update",
            Description = "Original description",
            Price = 199.99m,
            Stock = 10,
            CategoryId = 3,
            IsActive = true
        };

        var createResponse = await client.PostAsJsonAsync(
            "/api/products", product);

        var createdProduct = await createResponse.Content
            .ReadFromJsonAsync<Product>();

        Assert.NotNull(createdProduct);

        // Update the product
        createdProduct.Name = "Mouse After Update";
        createdProduct.Price = 249.99m;

        var updateResponse = await client.PutAsJsonAsync(
            $"/api/products/{createdProduct.Id}",
            createdProduct);

        Assert.Equal(
            System.Net.HttpStatusCode.OK,
            updateResponse.StatusCode);

        // Confirm the changes were saved
        var getResponse = await client.GetAsync(
            $"/api/products/{createdProduct.Id}");

        var updatedProduct = await getResponse.Content
            .ReadFromJsonAsync<Product>();

        Assert.NotNull(updatedProduct);
        Assert.Equal("Mouse After Update", updatedProduct.Name);
        Assert.Equal(249.99m, updatedProduct.Price);
    }

}
