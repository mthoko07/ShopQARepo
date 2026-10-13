
using System.ComponentModel.DataAnnotations;
using ShopQA.Models;
using Xunit;

namespace ShopQA.Tests;

public class ProductValidationTests
{
    [Fact]
    public void ValidProduct_ShouldPassValidation()
    {
        // Arrange
        var product = new Product
        {
            Name = "USB Hub",
            Description = "4-port USB hub",
            Price = 199.99m,
            Stock = 10,
            CategoryId = 3,
            IsActive = true
        };

        // Act
        var results = ValidateProduct(product);

        // Assert
        Assert.Empty(results);
    }

    [Fact]
    public void EmptyProductName_ShouldFailValidation()
    {
        var product = new Product
        {
            Name = "",
            Description = "Test product",
            Price = 100m,
            Stock = 10,
            CategoryId = 1
        };

        var results = ValidateProduct(product);

        Assert.Contains(results,
            result => result.MemberNames.Contains(nameof(Product.Name)));
    }

    [Fact]
    public void NegativePrice_ShouldFailValidation()
    {
        var product = new Product
        {
            Name = "USB Hub",
            Price = -10m,
            Stock = 10,
            CategoryId = 1
        };

        var results = ValidateProduct(product);

        Assert.Contains(results,
            result => result.MemberNames.Contains(nameof(Product.Price)));
    }

    [Fact]
    public void NegativeStock_ShouldFailValidation()
    {
        var product = new Product
        {
            Name = "USB Hub",
            Price = 100m,
            Stock = -1,
            CategoryId = 1
        };

        var results = ValidateProduct(product);

        Assert.Contains(results,
            result => result.MemberNames.Contains(nameof(Product.Stock)));
    }

    private static List<ValidationResult> ValidateProduct(Product product)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(product);

        Validator.TryValidateObject(
            product,
            context,
            results,
            validateAllProperties: true);

        return results;
    }


    //testing boundaries


    [Theory]
    [InlineData(0.01, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    [InlineData(1000000, true)]
    [InlineData(1000001, false)]
    public void Price_BoundaryValues(decimal price, bool isValid)
    {
        var product = new Product
        {
            Name = "USB Hub",
            Price = price,
            Stock = 10,
            CategoryId = 1
        };

        var results = ValidateProduct(product);

        var priceErrors = results
            .Where(result =>
                result.MemberNames.Contains(nameof(Product.Price)));

        if (isValid)
        {
            Assert.Empty(priceErrors);
        }
        else
        {
            Assert.NotEmpty(priceErrors);
        }
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(1, true)]
    [InlineData(100000, true)]
    [InlineData(-1, false)]
    [InlineData(100001, false)]
    public void Stock_BoundaryValues(int stock, bool isValid)
    {
        var product = new Product
        {
            Name = "USB Hub",
            Price = 100m,
            Stock = stock,
            CategoryId = 1
        };

        var results = ValidateProduct(product);

        var stockErrors = results
            .Where(result =>
                result.MemberNames.Contains(nameof(Product.Stock)));

        if (isValid)
        {
            Assert.Empty(stockErrors);
        }
        else
        {
            Assert.NotEmpty(stockErrors);
        }
    }

}
