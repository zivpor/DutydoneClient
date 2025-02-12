using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class GroupPage : ContentPage
{
	public GroupPage(GroupPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}