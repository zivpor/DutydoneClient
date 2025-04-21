using DutydoneClient.Models;
using DutydoneClient.Services;
using System.Runtime.CompilerServices;

namespace DutydoneClient.ViewModels;

public class ProfilePageViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;
    public ProfilePageViewModel(DutyDoneAPIProxy proxy)
	{
		this.proxy = proxy;
        //EditCommmand=new Command(OnEdit);
    }
    private User theUser;
    public User TheUser
    {
        get => theUser;
        set
        {
            theUser = value;
            OnPropertyChanged("TheUser");
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
    public Command EditCommmand { get; set; }
    //private void OnEdit() 
    //{
    //    ((App)Application.Current).MainPage.Navigation.PushAsync(<EditProfile>());
    //}
}