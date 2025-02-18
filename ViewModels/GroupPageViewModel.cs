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
                ReadDataFromServer();
                GroupName = group.GroupName;
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
    public GroupPageViewModel(DutyDoneAPIProxy proxy)
	{
        this.proxy = proxy;
        Tasks = new ObservableCollection<Models.Task>();
        
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

        List<Models.Task>? tasks = await proxy.GetGroupTasks(group);


        if (tasks != null)
        {
            Tasks.Clear();
            foreach (Models.Task t in tasks)
            {
                Tasks.Add(t);
            }
        }
    }
    
}