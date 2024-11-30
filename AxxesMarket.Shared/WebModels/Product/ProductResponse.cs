namespace AxxesMarket.Shared.WebModels.Product;
public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public DateTime? SoldOn { get; set; }
    public string ImageUrl { get; set; }

    public bool IsAvailable => SoldOn is null;
}
