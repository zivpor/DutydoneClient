using DutydoneClient.Models;
using DutydoneClient.Services;

namespace DutydoneClient.ViewModels;

public class ManageredGroupPageViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;

    public Command AddPeopleCommand { get; }
    public Command AddTaskCommand { get; }

    public ManageredGroupPageViewModel(DutyDoneAPIProxy proxy)
	{
        this.proxy = proxy;
        AddPeopleCommand = new Command(AddPeople);
        AddTaskCommand = new Command(AddTask);
        Group g= ((App)Application.Current).SelectedGroup;
        GroupName = g.GroupName;

    }
    private string groupName;
    public string GroupName
    {
        get => groupName;
        set
        {
            groupName = value;
            OnPropertyChanged("groupName");
        }
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