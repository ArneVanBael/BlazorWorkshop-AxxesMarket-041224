using AxxesMarket.SPA.Client.Shared.Toasts;
using BlazorState;
using System.Collections.ObjectModel;

namespace AxxesMarket.Shared.Components.Store;

public partial class ApplicationState : State<ApplicationState>
{
    public bool IsMenuVisible { get; set; } = false;
    public string LoggedInUserName { get; set; }
    public string LoggedInUserEmail { get; set; }
    public string ApplicationName { get; set; }
    public bool IsModalOpen { get; set; }
    public bool IsLoaded { get; set; }

    private List<ToastMessageItem> _toastMessages = new List<ToastMessageItem>();
    public IReadOnlyCollection<ToastMessageItem> ToastMessages => new ReadOnlyCollection<ToastMessageItem>(_toastMessages);

    public override void Initialize()
    {
    }

    public void OpenModal() => IsModalOpen = true;
    public void CloseModal() => IsModalOpen = false;

    public void AddToastMessage(ToastMessageItem message) => _toastMessages.Add(message);
    public void RemoveToastMessage(Guid id)
    {
        var toast = _toastMessages.SingleOrDefault(x => x.Id == id);
        if (toast is null) return;
        _toastMessages.Remove(toast);
    }
}
