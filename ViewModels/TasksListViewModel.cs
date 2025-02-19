using DutydoneClient.Models;
using DutydoneClient.Services;
using System.Collections.ObjectModel;

namespace DutydoneClient.ViewModels;

public class TasksListViewModel : ViewModelBase
{
	public Command FilterCommand { get; set; }
	private DutyDoneAPIProxy proxy;
	private List<Models.Task> allTasks;
	private ObservableCollection<Models.Task> filterdTasks;
    public ObservableCollection<Models.Task> FilterdTasks
	{
		get
		{
			return this.filterdTasks;
		}
		set
		{
			this.filterdTasks = value;
			OnPropertyChanged();
		}
	}
    public TasksListViewModel(DutyDoneAPIProxy proxy)
	{
		this.proxy = proxy;
		this.FilterCommand = new Command(Filter);
		allTasks = new List<Models.Task>();
		FilterdTasks = new ObservableCollection<Models.Task>();
		filtered = false;
		ReadTasks();
	}
	private async void ReadTasks()
	{
		allTasks = await proxy.GetTasks();
		if (allTasks == null)
			allTasks = new List<Models.Task>();
	}
	private bool filtered;
	public async void Filter()
	{
		filtered = true;
		FilterdTasks.Clear();
		foreach(Models.Task t in this.allTasks)
		{
			if()
		}
	}
}