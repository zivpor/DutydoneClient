using System.Collections.ObjectModel;
using DutydoneClient.Models;
using DutydoneClient.Services;
namespace DutydoneClient.ViewModels;

public class AdminPageViewModel : ContentPage
{
    private int blockCount = 0;
    private DutyDoneAPIProxy proxy;
    public AdminPageViewModel(DutyDoneAPIProxy proxy)
    {
        this.proxy = proxy;
        Users = new ObservableCollection<User>();
        User u = ((App)Application.Current).LoggedInUser;
        if (u.IsAdmin == true)
        {
            IsAdmin = true;
        }
        else
        {
            IsAdmin = false;
        }
        BlockPic = "blocked.png";
        BlockCommand = new Command<User>(OnBlock);
        
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


    //#region Single Selection


    //private User selectedUser1;
    //public User SelectedUser1
    //{
    //    get
    //    {
    //        return this.selectedUser1;
    //    }
    //    set
    //    {
    //        this.selectedUser1 = value;
    //        OnSingleSelectUser(selectedUser1);
    //        OnPropertyChanged();
    //    }
    //}



    //private async void OnSingleSelectUser(User p)
    //{
    //    if (p != null)
    //    {
    //        var navParam = new Dictionary<string, object>
    //            {
    //                {"selectedUser1",p }
    //            };
    //        await Shell.Current.GoToAsync("UserInfo", navParam);

    //        SelectedUser1 = null;

    //    }
    //}
    //#endregion
    #endregion


    #region Filter

    public void Sort()
    {
        if (UserName != null || UserName != "")
        {
            List<User> temp = allUsers.Where(u => u.Username.Contains(UserName)).ToList();
            Users = new ObservableCollection<User>(temp);
        }
        else
        {
            Users = new ObservableCollection<User>(allUsers);
        }

    }
    private string userName;
    public string UserName
    {
        get
        {
            return userName;
        }
        set
        {
            userName = value;
            //Sort();
            OnPropertyChanged("UserName");
            Sort();
        }
    }
    #endregion


    #region Block
    private string blockPic;
    public string BlockPic
    {
        get => blockPic;
        set
        {
            blockPic = value;
            OnPropertyChanged(nameof(BlockPic));
        }
    }

    private bool isAdmin;
    public bool IsAdmin
    {
        get => isAdmin;
        set
        {
            isAdmin = value;
            OnPropertyChanged(nameof(IsAdmin));
        }
    }
    public Command BlockCommand { get; }
    public async void OnBlock(User u)
    {
        blockCount++;
        // Switch between different images based on the click count
        if (blockCount % 2 == 0)
        {
            u.IsBlocked = false;
            await proxy.Block(u);
            BlockPic = "block.png"; // First image
            u.ProfileImagePath = "";//לשאול את עופר
        }
        else
        {
            u.IsBlocked = true;
            await proxy.Block(u);
            BlockPic = "unblock.png"; // Second image
            u.ProfileImagePath = "";//לשאול את עופר
        }

    }
    #endregion
}