using DutydoneClient.Services;
using DutydoneClient.Models;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;


namespace DutydoneClient.ViewModels;

public class CreateGroupViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;
    public ObservableCollection<GroupType> GroupTypes { get; set; }
    public CreateGroupViewModel(DutyDoneAPIProxy proxy)
    {
        this.GroupTypes = new ObservableCollection<GroupType>(((App)Application.Current).BasicData.GroupTypes);
        SelectedGroupType = this.GroupTypes[0];
        this.proxy = proxy;
        CreateCommand = new Command(Create);
        NameError = "Name is required";
        UploadPhotoCommand = new Command(OnUploadPhoto);
        PhotoURL = proxy.GetDefaultGroupProfilePhotoUrl();
        LocalPhotoPath = "";
        

    }
    public Command CreateCommand { get; }
    private bool showNameError;
    #region Name
    public bool ShowNameError
    {
        get => showNameError;
        set
        {
            showNameError = value;
            OnPropertyChanged("ShowNameError");
        }
    }
    private string name;

    public string Name
    {
        get => name;
        set
        {
            name = value;
            ValidateName();
            OnPropertyChanged("Name");
        }
    }
    private string nameError;

    public string NameError
    {
        get => nameError;
        set
        {
            nameError = value;
            OnPropertyChanged("NameError");
        }
    }
    private void ValidateName()
    {
        this.ShowNameError = string.IsNullOrEmpty(Name);
    }
    #endregion
    #region Group Type
    private bool showTypeError;

    public bool ShowTypeError
    {
        get => showTypeError;
        set
        {
            showNameError = value;
            OnPropertyChanged("ShowNameError");
        }
    }

    private GroupType selectedGroupType;

    public GroupType SelectedGroupType
    {
        get => selectedGroupType;
        set
        {
            selectedGroupType = value;
            OnPropertyChanged("SelectedGroupType");
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

    public Command UploadPhotoCommand { get; }
    //This method open the file picker to select a photo
    private async void OnUploadPhoto()
    {
        try
        {
            var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Please select a photo",
            });

            if (result != null)
            {
                // The user picked a file
                this.LocalPhotoPath = result.FullPath;
                this.PhotoURL = result.FullPath;
            }
        }
        catch (Exception ex)
        {
        }

    }

    private void UpdatePhotoURL(string virtualPath)
    {
        Random r = new Random();
        PhotoURL = proxy.GetImagesBaseAddress() + virtualPath + "?v=" + r.Next();
        LocalPhotoPath = "";
    }

    #endregion
    public async void Create()
    {
        ValidateName();

        


        if (!ShowNameError)
        {
           
            var newGroup = new Models.Group()
            {
                GroupName = Name,
                GroupType = SelectedGroupType.GroupTypeId,
                GroupAdmin = ((App)Application.Current).LoggedInUser.UserId,
                

            };

            //Call the Register method on the proxy to register the new user
            InServerCall = true;
            int? groupId = await proxy.CreateGroup(newGroup);
            InServerCall = false;

            //If the registration was successful, navigate to the login page
            if (groupId != null)
            {

                InServerCall = false;
                newGroup.GroupId = groupId.Value;
                if (localPhotoPath != null)
                {
                    

                    newGroup = await proxy.UploadGroupProfileImage(LocalPhotoPath);
                    if (newGroup == null)
                    {
                        string errorMsg = "Adding group succeeded but photo failed to be uploaded.";
                        await Application.Current.MainPage.DisplayAlert("Adding group", errorMsg, "ok");
                    }
                }
               ((App)(Application.Current)).MainPage.Navigation.PopAsync();
            }
            else
            {

                //If the registration failed, display an error message
                string errorMsg = "Adding Group failed. Please try again.";
                await Application.Current.MainPage.DisplayAlert("Adding Group", errorMsg, "ok");
            }


        }
    }
}