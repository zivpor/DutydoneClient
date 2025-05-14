using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class Calender : ContentPage
{
	public Calender(CalenderViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}