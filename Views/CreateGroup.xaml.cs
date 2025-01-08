using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class CreateGroup : ContentPage
{
    public CreateGroup(CreateGroupViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}