using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class Register : ContentPage
{
    public Register(RegisterViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}