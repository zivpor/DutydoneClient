using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class EditProfile : ContentPage
{
	public EditProfile(EditProfileViewModel vm)
	{
		this.BindingContext= vm;
		InitializeComponent();
	}
}