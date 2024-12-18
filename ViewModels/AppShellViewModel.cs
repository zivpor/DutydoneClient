using DutydoneClient.Models;
using DutydoneClient.Views;

namespace DutydoneClient.ViewModels;

public class AppShellViewModel : ViewModelBase
{
    private User? currentUser;
    private IServiceProvider serviceProvider;
    public AppShellViewModel(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.currentUser = ((App)Application.Current).LoggedInUser;
    }

    public bool IsAdmin
    {
        get
        {
            return this.currentUser.IsAdmin;
        }
    }

    //this command will be used for logout menu item
    public Command LogoutCommand
    {
        get
        {
            return new Command(OnLogout);
        }
    }
    //this method will be trigger upon Logout button click
    public void OnLogout()
    {
        ((App)Application.Current).LoggedInUser = null;

        ((App)Application.Current).MainPage = new NavigationPage(serviceProvider.GetService<Login>());
    }
}