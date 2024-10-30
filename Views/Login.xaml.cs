using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class Login : ContentPage
{
    public Login(LoginViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}