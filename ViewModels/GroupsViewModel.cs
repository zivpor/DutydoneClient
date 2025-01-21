using DutydoneClient.Models;
using DutydoneClient.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DutydoneClient.ViewModels;

public class GroupsViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;
    private IServiceProvider serviceProvider;
    private ObservableCollection<Models.Group> groups;
    
    public ObservableCollection<Models.Group> Groups
    {
        get => groups;
        set
        {
            groups = value;
            OnPropertyChanged("Groups");
        }
    }
    private ObservableCollection<Models.Group> managerGroups;
    public ObservableCollection<Models.Group> ManagerGroups
    {
        get => managerGroups;
        set
        {
            managerGroups = value;
            OnPropertyChanged("ManagerGroups");
        }
    }

    private Group managerSelectedGroup;
    public Group ManagerSelectedGroup
    {
        get => managerSelectedGroup;
        set
        {
            managerSelectedGroup = value;
            if (value != null)
            {
                GoToGroup(value);
            }
            OnPropertyChanged("ManagerSelectedGroup");
        }
    }
    private Group selectedGroup;
    public Group SelectedGroup
    {
        get => selectedGroup;
        set
        {
            selectedGroup = value;
            if (value != null) 
            {
                GoToGroup(value);
            }
            OnPropertyChanged("SelectedGroup");
        }
    }

    public GroupsViewModel(DutyDoneAPIProxy proxy, IServiceProvider serviceProvider)
	{
        this.serviceProvider = serviceProvider;
        this.proxy = proxy;
        CreateGroupCommand = new Command(GoToCreateGroup);
        ManagerGroups = new ObservableCollection<Models.Group>();
        Groups = new ObservableCollection<Models.Group>();
        SelectedGroup = null;
        managerSelectedGroup = null;
        ReadDataFromServer();
    }
    private async void ReadDataFromServer()
    {
        
        List<Models.Group>? groups = await proxy.GetGroups();
        List<Models.Group>? managerGroups = await proxy.GetManagerGroups();

        if (groups != null)
        {
            Groups.Clear();
            foreach (Models.Group g in groups)
            {
                Groups.Add(g);
            }
        }

        if (managerGroups != null)
        {
            ManagerGroups.Clear();
            foreach (Models.Group g in managerGroups)
            {
                ManagerGroups.Add(g);
            }
        }
    }
    public ICommand CreateGroupCommand { get; set; }
    private async void GoToCreateGroup()
    {
        await Shell.Current.GoToAsync("createGroup");
    }
    private async void GoToGroup(Group g)
    {
        if (g != null)
        {
            User currentUser = ((App)Application.Current).LoggedInUser;
            if (g.GroupAdmin == currentUser.UserId)
            {
                var navParam = new Dictionary<string, object>
                {
                    { "managerSelectedGroup", g }
                };
                //Navigate to the task details page
                await Shell.Current.GoToAsync("manageredGroupPage", navParam);

            }
            else
            {
                var navParam = new Dictionary<string, object>
                {
                    { "selectedGroup", g }
                };
                //Navigate to the task details page
                await Shell.Current.GoToAsync("groupPage", navParam);

            }
        }
            SelectedGroup = null;
            ManagerSelectedGroup = null;
        
    }

    public void Refresh()
    {
        ReadDataFromServer();
    }
}