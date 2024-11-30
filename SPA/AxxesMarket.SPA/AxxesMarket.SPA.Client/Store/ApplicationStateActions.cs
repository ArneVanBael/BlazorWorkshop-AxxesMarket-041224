using BlazorState;

namespace AxxesMarket.SPA.Client.Store;

public partial class ApplicationState
{
    public class LoadInitialApplicationStateAction: IAction
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string ApplicationName { get; set; }

        public LoadInitialApplicationStateAction(string applicationName)
        {
            ApplicationName = applicationName;
        }
    }

    public class ToggleSidebarMenuAction : IAction
    {
    }

    public class OpenCloseModalAction: IAction
    {

        public bool IsOpen;

        public OpenCloseModalAction(bool isOpen)
        {
            IsOpen = isOpen;
        }
    }

    public class AddSuccessToastAction : IAction
    {
        public string Text { get; set; }

        public AddSuccessToastAction(string text)
        {
            Text = text;
        }
    }

    public class AddErrorToastAction : IAction
    {
        public string Text { get; set; }

        public AddErrorToastAction(string text)
        {
            Text = text;
        }
    }

    public class RemoveToastAction : IAction
    {
        public Guid Id { get; set; }

        public RemoveToastAction(Guid id)
        {
            Id = id;
        }
    }
}
