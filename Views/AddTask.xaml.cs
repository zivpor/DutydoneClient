using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class AddTask : ContentPage
{
	public AddTask(AddTaskViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}