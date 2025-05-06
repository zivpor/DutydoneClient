using DutydoneClient.Models;
using DutydoneClient.Services;
using System.Collections.ObjectModel;

namespace DutydoneClient.ViewModels;

public class AddPeopleViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;
   
    public AddPeopleViewModel(DutyDoneAPIProxy proxy)
	{
        this.proxy = proxy;
        Users = new ObservableCollection<User>();
        ReadUsers();
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
        List<User> list = await proxy.GetUsers();
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
}