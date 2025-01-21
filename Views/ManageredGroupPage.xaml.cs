using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class ManageredGroupPage : ContentPage
{
	public ManageredGroupPage(ManageredGroupPageViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}