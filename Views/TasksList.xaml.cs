using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class TasksList : ContentPage
{
	public TasksList(TasksListViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}