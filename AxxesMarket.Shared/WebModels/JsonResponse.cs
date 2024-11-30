namespace AxxesMarket.Shared.WebModels;
public class JsonResponse
{
    public bool Success => !Errors.Any();
    public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
}

public class JsonResponse<T> : JsonResponse
{
    public T Result { get; set; }
}
