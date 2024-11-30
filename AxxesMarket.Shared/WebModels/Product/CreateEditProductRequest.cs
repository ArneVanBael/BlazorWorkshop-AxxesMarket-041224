using System.ComponentModel.DataAnnotations;

namespace AxxesMarket.Shared.WebModels.Product;
public class CreateEditProductRequest
{
    public Guid? Id { get; set; }

    [Required]
    [Display(Name = "name")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "description")]
    public string? Description { get; set; }

    [Required]
    [Display(Name = "detailed Description")]
    public string? DetailedDescription { get; set; }

    [Required]
    [Display(Name = "purchase date")]
    public DateTime? PurchageDate { get; set; }

    [Required]
    [Display(Name = "has warranty")]
    public bool HasWaranty { get; set; }

    [Required]
    [Display(Name = "price")]
    public double? Price { get; set; }

    //[Required]
    //[Display(Name = "")]
    //public string? ImageUrl { get; set; }
}
