using DutydoneClient.Models;
using DutydoneClient.Services;

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

    public ManageredGroupPageViewModel(DutyDoneAPIProxy proxy)
	{
        this.proxy = proxy;
        AddPeopleCommand = new Command(AddPeople);
        AddTaskCommand = new Command(AddTask);
        
       

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
    private async void InFieldDataAsync()
    {
        GroupName = Group.GroupName;
    }
    private async void AddTask()
    {
        await Shell.Current.GoToAsync("addTask");
    }

    private async void AddPeople()
    {
        await Shell.Current.GoToAsync("addPeople");
    }
}