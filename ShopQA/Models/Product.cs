using System.ComponentModel.DataAnnotations;

namespace ShopQA.Models;


public class Product
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, MinimumLength = 2,
       ErrorMessage = "Product name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;
    [StringLength(500,
       ErrorMessage = "Description cannot exceed 500 characters.")]
    public string Description { get; set; } = string.Empty;
    [Range(typeof(decimal), "0.01", "1000000",
       ErrorMessage = "Price must be greater than zero and no more than 1,000,000.")]
    public decimal Price { get; set; }
    [Range(0, 100000,
      ErrorMessage = "Stock must be between 0 and 100,000.")]
    public int Stock { get; set; }
    [Range(1, int.MaxValue,
        ErrorMessage = "A valid category is required.")]
    public int CategoryId { get; set; }

    public bool IsActive { get; set; } = true;

    public Category? Category { get; set; }
}