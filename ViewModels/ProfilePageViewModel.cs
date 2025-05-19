using DutydoneClient.Models;
using DutydoneClient.Services;
using DutydoneClient.Views;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DutydoneClient.ViewModels;

[QueryProperty("TheUser", "TheUser")]
public class ProfilePageViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;
    public ProfilePageViewModel(DutyDoneAPIProxy proxy)
	{
		this.proxy = proxy;
        EditCommand = new Command(OnEdit);
        TheUser = ((App)Application.Current).LoggedInUser;
    }
    
    private User theUser;
    public User TheUser
    {
        get => theUser;
        set
        {
            theUser = value;
            if (theUser != null)
            {
                User LoggedInUser = ((App)Application.Current).LoggedInUser;
                CanEdit = (LoggedInUser.UserId == theUser.UserId);
                Name = theUser.Username;
                PhotoURL = theUser.FullImageUrl;

            }
            OnPropertyChanged("TheUser");
        }
    }

    private bool canEdit;
    public bool CanEdit
    {
        get => canEdit;
        set
        {
            canEdit = value;
            OnPropertyChanged();
        }
    }

    #region Name
    private string name;
    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged("Name");
        }
    }
    #endregion
    #region Photo
    private string photoURL;
    public string PhotoURL
    {
        get => photoURL;
        set
        {
            photoURL = value;
            OnPropertyChanged("PhotoURL");
        }
    }
    private string localPhotoPath;
    public string LocalPhotoPath
    {
        get => localPhotoPath;
        set
        {
            localPhotoPath = value;
            OnPropertyChanged("LocalPhotoPath");
        }
    }
    #endregion
    public ICommand EditCommand { get; set; }//EditCommand
    private async void OnEdit()
    {
        await Shell.Current.GoToAsync("editProfile");
    }

    public override void Refresh()
    {
        base.Refresh();
        TheUser = ((App)Application.Current).LoggedInUser;
    }
}