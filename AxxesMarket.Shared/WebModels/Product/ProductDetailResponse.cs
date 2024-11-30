namespace AxxesMarket.Shared.WebModels.Product;
public class ProductDetailResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string DetailedDescription { get; set; }
    public DateTime PurchageDate { get; set; }
    public bool HasWaranty { get; set; }
    public double Price { get; set; }
    public DateTime? SoldOn { get; set; }
    public string ImageUrl { get; set; }
}
