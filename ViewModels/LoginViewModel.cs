using DutydoneClient.Models;
using System.Windows.Input;
using System.Collections.Generic;
using DutydoneClient.Services;
using Microsoft.Win32;
using DutydoneClient.Views;


namespace DutydoneClient.ViewModels;

public class LoginViewModel : ViewModelBase
{

    private DutyDoneAPIProxy proxy;
    private IServiceProvider serviceProvider;
    public LoginViewModel(DutyDoneAPIProxy proxy, IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.proxy = proxy;
        LoginCommand = new Command(OnLogin);
        RegisterCommand = new Command(OnRegister);
        email = "";
        password = "";
        InServerCall = false;
        errorMsg = "";
    }

    public ICommand LoginCommand { get; set; }
    public ICommand RegisterCommand { get; }
    private string email;
    public string Email
    {
        get => email;
        set
        {
            if (email != value)
            {
                email = value;
                OnPropertyChanged(nameof(Email));
                // can add more logic here like email validation etc.
                // can add error message property and set it here
            }
        }
    }
    private string password;
    public string Password
    {
        get => password;
        set
        {
            if (password != value)
            {
                password = value;
                OnPropertyChanged(nameof(Password));
                // can add more logic here like email validation etc.
                // can add error message property and set it here
            }
        }
    }
    private string errorMsg;
    public string ErrorMsg
    {
        get => errorMsg;
        set
        {
            if (errorMsg != value)
            {
                errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
            }
        }
    }
    private async void OnLogin()
    {
        //Choose the way you want to blobk the page while indicating a server call
        InServerCall = true;
        ErrorMsg = "";
        //Call the server to login
        LoginInfo loginInfo = new LoginInfo { Email=Email , Password = Password };
        User? u = await this.proxy.LoginAsync(loginInfo);

        InServerCall = false;

        //Set the application logged in user to be whatever user returned (null or real user)
        ((App)Application.Current).LoggedInUser = u;
        if (u == null)
        {
            ErrorMsg = "Invalid email or password";
        }
        else
        {
            ErrorMsg = "";
            //Navigate to the main page
            AppShell shell = serviceProvider.GetService<AppShell>();
            
            ((App)Application.Current).MainPage = shell;
            Shell.Current.FlyoutIsPresented = false; //close the flyout
           
        }
    }
    private void OnRegister()
    {
        ErrorMsg = "";
        Email = "";
        Password = "";
        // Navigate to the Register View page
        ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<Register>());
    }
}