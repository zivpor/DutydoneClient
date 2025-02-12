using DutydoneClient.Models;
using DutydoneClient.Services;
using System.Collections.ObjectModel;

namespace DutydoneClient.ViewModels;
[QueryProperty("Group", "managerSelectedGroup")]
public class ManageredGroupPageViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;

    public Command AddPeopleCommand { get; }
    public Command AddTaskCommand { get; }

    private Group group;
    public Group Group
    {
        get => group;
        set
        {
            if (group != value)
            {
                group = value;
                InFieldDataAsync();
                OnPropertyChanged(nameof(Group));
            }
        }
    }
    private ObservableCollection<Models.Task> tasks;

    public ObservableCollection<Models.Task> Tasks
    {
        get => tasks;
        set
        {
            tasks = value;
            OnPropertyChanged("Tasks");
        }
    }
    public ManageredGroupPageViewModel(DutyDoneAPIProxy proxy)
	{
        this.proxy = proxy;
        AddPeopleCommand = new Command(AddPeople);
        AddTaskCommand = new Command(AddTask);
        Tasks = new ObservableCollection<Models.Task>();
        ReadDataFromServer();

    }
    private string groupName;
    public string GroupName
    {
        get => groupName;
        set
        {
            groupName = value;
            OnPropertyChanged();
        }
    }
    private async void ReadDataFromServer()
    {

        List<Models.Task>? tasks = await proxy.GetGroupTasks();


        if (tasks != null)
        {
            Tasks.Clear();
            foreach (Models.Task t in tasks)
            {
                tasks.Add(t);
            }
        }
    }
    private async void InFieldDataAsync()
    {
        GroupName = Group.GroupName;
    }
    private async void AddTask()
    {
        var navParam = new Dictionary<string, object>
        {
            { "group", Group }
        };
        await Shell.Current.GoToAsync("addTask", navParam);
    }

    private async void AddPeople()
    {
        await Shell.Current.GoToAsync("addPeople");
    }
}