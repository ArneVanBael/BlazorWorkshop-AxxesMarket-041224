namespace AxxesMarket.SPA.Client.Utils;

public static class ApiRoutes
{
    public static string GetProducts() => "api/products";
    public static string GetMyProducts() => "api/products/my";
    public static string GetProduct(Guid id) => $"api/products/{id}";
    public static string CreateProduct() => "api/products";
    public static string UpdateProduct(Guid id) => $"api/products/{id}";
    public static string BuyProduct(Guid id) => $"api/products/{id}/buy";
}
