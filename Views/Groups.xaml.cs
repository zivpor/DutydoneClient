using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class Groups : ContentPage
{
	public Groups(GroupsViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}