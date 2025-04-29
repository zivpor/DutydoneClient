using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class AddPeople : ContentPage
{
	public AddPeople(AddPeopleViewModel vm)
	{
		this.BindingContext = vm;
        InitializeComponent();
	}
}