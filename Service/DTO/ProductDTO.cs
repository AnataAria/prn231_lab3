using System.ComponentModel.DataAnnotations;

namespace Service.DTO;

public class ProductResponse
{
    [Required(ErrorMessage = "Product ID is required.")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, ErrorMessage = "Product name can't be longer than 100 characters.")]
    public string? ProductName { get; set; }

    [Required(ErrorMessage = "Category ID is required.")]
    public int CategoryId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Units in stock must be a non-negative integer.")]
    public int UnitsInStock { get; set; }

    [Range(0.01, 10000, ErrorMessage = "Unit price must be between 0.01 and 10,000.")]
    public decimal UnitPrice { get; set; }
}

public class ProductRequest
{
    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, ErrorMessage = "Product name can't be longer than 100 characters.")]
    public string? ProductName { get; set; }

    [Required(ErrorMessage = "Category ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Category ID must be more than 1")]
    public int CategoryId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Units in stock must be a non-negative integer.")]
    public int UnitsInStock { get; set; }

    [Range(0.01, 10000, ErrorMessage = "Unit price must be between 0.01 and 10,000.")]
    public decimal UnitPrice { get; set; }
}