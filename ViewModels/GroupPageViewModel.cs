using DutydoneClient.Models;
using DutydoneClient.Services;
using System.Collections.ObjectModel;

namespace DutydoneClient.ViewModels;
 
[QueryProperty("Group", "selectedGroup")]
public class GroupPageViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;
    private Group group;
    public Group Group
    {
        get => group;
        set
        {
            if (group != value)
            {
                group = value;
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
    public GroupPageViewModel()
	{
        this.proxy = proxy;
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
            InFieldDataAsync();
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
}