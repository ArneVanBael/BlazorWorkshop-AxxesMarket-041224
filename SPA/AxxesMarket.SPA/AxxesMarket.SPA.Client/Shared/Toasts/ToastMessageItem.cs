namespace AxxesMarket.SPA.Client.Shared.Toasts;

public class ToastMessageItem
{
    public Guid Id { get;} = Guid.NewGuid();
    public string Text { get; set; }
    public ToastMessageType Type { get; set; }
    public DateTime CreatedOn { get; } = DateTime.Now;
}

public enum ToastMessageType
{
    Success,
    Error
}
