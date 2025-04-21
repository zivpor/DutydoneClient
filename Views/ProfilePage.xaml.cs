using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfilePageViewModel vm)
	{
		this.BindingContext = vm;
        InitializeComponent();
	}
}