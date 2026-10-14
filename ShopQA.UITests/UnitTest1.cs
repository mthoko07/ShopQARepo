
using Microsoft.Playwright;
using Xunit;

namespace ShopQA.UITests;

public class StorefrontTests
{
    [Fact]
    public async Task Storefront_HomePageLoads_Successfully()
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = true
            });

        var page = await browser.NewPageAsync();

        await page.GotoAsync("https://localhost:7091/");

        // Verify the main storefront heading.
        await Assertions.Expect(
            page.GetByRole(
                AriaRole.Heading,
                new() { Name = "Welcome to ShopQA" }))
            .ToBeVisibleAsync();

        // Verify the product section exists.
        await Assertions.Expect(
            page.GetByRole(
                AriaRole.Heading,
                new() { Name = "Our Products" }))
            .ToBeVisibleAsync();

        // Verify the cart navigation link exists.
        await Assertions.Expect(
            page.GetByRole(
                AriaRole.Link,
                new() { Name = "My Cart" }))
            .ToBeVisibleAsync();
    }
}
