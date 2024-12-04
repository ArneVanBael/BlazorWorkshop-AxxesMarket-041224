namespace AxxesMarket.Api.Domain;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string DetailedDescription { get; private set; }
    public DateTime PurchageDate { get; private set; }
    public bool HasWaranty { get; private set; }
    public double Price { get; private set; }
    public DateTime? SoldOn { get; private set; }
    public string ImageUrl { get; private set; }
    public string Owner { get; private set; }

    public bool IsAvailable => SoldOn is null;

    public void Update(Product product)
    {
        PurchageDate = product.PurchageDate;
        Price = product.Price;
        Name = product.Name;
        Description = product.Description;
        DetailedDescription = product.DetailedDescription;
        HasWaranty = product.HasWaranty;
    }

    public static Product Create(Guid? id, string name, string description, string detailedDescription, DateTime purchageDate, bool hasWaranty, double price, DateTime? soldOn, string imageUrl, string? owner = null)
    {
        var product = new Product
        {
            Id = id ?? default,
            Name = name,
            Description = description,
            DetailedDescription = detailedDescription,
            PurchageDate = purchageDate,
            HasWaranty = hasWaranty,
            Price = price,
            SoldOn = soldOn,
            ImageUrl = imageUrl,
        };

        if(string.IsNullOrWhiteSpace(owner))
        {
            // get logged in user to set as owner
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                product.Owner = Thread.CurrentPrincipal.Identity.Name;
            }
            else
            {
                product.Owner = "John Doe";
            }
        } else
        {
            product.Owner = owner;
        }

        return product;
    }

    public void Buy()
    {
        SoldOn = DateTime.Now;
    }
}
