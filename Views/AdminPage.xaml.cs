using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminPageViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}