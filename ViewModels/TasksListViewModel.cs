using DutydoneClient.Models;
using DutydoneClient.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DutydoneClient.ViewModels;

public class TasksListViewModel : ViewModelBase
{
	public Command FilterCommand { get; set; }
	private DutyDoneAPIProxy proxy;
	private List<Models.Task> allTasks;
	private ObservableCollection<Models.Task> filterdTasks;
    public ObservableCollection<TaskType> TaskTypes { get; set; }
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
    private TaskType selectedType;
    public Object SelectedType
    {
        get => selectedType;
        set
        {
			if (value is TaskType)
				selectedType = (TaskType)value;
			Filter();
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
        this.TaskTypes = new ObservableCollection<TaskType>(((App)Application.Current).BasicData.TaskTypes);
		this.TaskTypes.Add(new TaskType()
		{
			TaskTypeId = 0,
			TaskTypeName = "All"
		});
        SelectedType = this.TaskTypes[TaskTypes.Count-1];
        ReadTasks();
	}
    public ICommand DoneCommand => new Command<Models.Task>(OnDone);
    void OnDone(Models.Task t)
    {
        if (FilterdTasks.Contains(t))
        {
			int i = FilterdTasks.IndexOf(t);
			FilterdTasks.Remove(t);
			t.StatusId = 3;
			FilterdTasks.Insert(i, t);
        }
    }
    private async void ReadTasks()
	{
		allTasks = await proxy.GetTasks();
		if (allTasks == null)
			allTasks = new List<Models.Task>();
		Filter();
	}
	private bool filtered;
	public async void Filter()
	{
		//filtered = true;
		filterdTasks.Clear();
		foreach (Models.Task t in this.allTasks)
		{
			if (selectedType.TaskTypeId == 0 || t.TaskType == selectedType.TaskTypeId)
			{
				FilterdTasks.Add(t);
			}
		}
	}
    public override void Refresh()
    {
        base.Refresh();
		ReadTasks();
    }
}