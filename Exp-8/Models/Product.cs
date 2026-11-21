using System.ComponentModel.DataAnnotations;

public class Product
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [Display(Name = "Product Name")]
    public string ProductName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category is required")]
    public string Category { get; set; } = string.Empty;

    [Range(0, 100000, ErrorMessage = "Quantity must be 0 or greater")]
    public int Quantity { get; set; }

    [Range(0.01, 1000000, ErrorMessage = "Price must be greater than 0")]
    [Display(Name = "Unit Price")]
    public decimal UnitPrice { get; set; }
}
