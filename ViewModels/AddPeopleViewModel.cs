using DutydoneClient.Models;
using DutydoneClient.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks.Dataflow;

namespace DutydoneClient.ViewModels;
[QueryProperty("Group", "group")]
public class AddPeopleViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;
    private string usersEmail;
    public string UsersEmail
    {
        get => usersEmail;
        set
        {
            if (usersEmail != value)
            {
                usersEmail = value;
                OnPropertyChanged("UsersEmail");
            }
        }
    }

   
    public AddPeopleViewModel(DutyDoneAPIProxy proxy)
	{
        this.proxy = proxy;
        Users = new ObservableCollection<User>();
        
        AddCommand = new Command(OnAdd);
    }

    private Group group;
    public Group Group
    {
        get => group;
        set
        {
            if (group != value)
            {
                group = value;
                ReadUsers();
                OnPropertyChanged(nameof(Group));
            }
        }
    }

    #region Collection View of Users
    private List<User> allUsers;
    private ObservableCollection<User> users;
    public ObservableCollection<User> Users
    {
        get
        {
            return this.users;
        }
        set
        {
            this.users = value;
            OnPropertyChanged(nameof(Users));
        }
    }


    private async void ReadUsers()
    {
        List<User> list = await proxy.GetUsersInGroup(group);
        if (list == null)
            return;
        this.Users = new ObservableCollection<User>(list);
        allUsers = list;
    }


    #region Single Selection


    private User selectedUser;
    public User SelectedUser
    {
        get
        {
            return this.selectedUser;
        }
        set
        {
            this.selectedUser = value;
            OnSingleSelectUser(selectedUser);
            OnPropertyChanged();
        }
    }



    private async void OnSingleSelectUser(User p)
    {
        if (p != null)
        {
            var navParam = new Dictionary<string, object>
                {
                    {"TheUser",p }
                };
            await Shell.Current.GoToAsync("Profile", navParam);

            SelectedUser = null;

        }
    }
    #endregion
    #endregion
    public Command AddCommand { get; }
    public async void OnAdd()
    {
        bool success = await proxy.AddUserToGroup(UsersEmail, Group.GroupId);
        if (success)
        {
            ReadUsers();
            await Shell.Current.DisplayAlert("Add User", "The User was added to the group", "ok");
        }
        else
        {
            await Shell.Current.DisplayAlert("Add User Error", "The User was NOT added to the group", "ok");
        }
    }
}