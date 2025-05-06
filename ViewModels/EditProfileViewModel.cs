using DutydoneClient.Models;
using DutydoneClient.Services;
using DutydoneClient.Views;

namespace DutydoneClient.ViewModels;

public class EditProfileViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;

    private IServiceProvider serviceProvider;
    public EditProfileViewModel(DutyDoneAPIProxy proxy, IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.proxy = proxy;
        EditCommand = new Command(OnEdit);
        SaveCommand = new Command(OnSave);
        User u = ((App)Application.Current).LoggedInUser;
        User = u;
        LocalPhotoPath = "";
        InServerCall = false;
    }

    #region User
    private User user;
    public User User
    {
        get => user;
        set
        {
            if (user != value)
            {
                user = value;
                InItFieldsDataAsync();
                OnPropertyChanged(nameof(User));
            }
        }
    }


    #endregion

    #region Name
    private bool showNameError;

    public bool ShowNameError
    {
        get => showNameError;
        set
        {
            showNameError = value;
            OnPropertyChanged(nameof(ShowNameError));
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
            OnPropertyChanged(nameof(Name));
        }
    }

    private string nameError;

    public string NameError
    {
        get => nameError;
        set
        {
            nameError = value;
            OnPropertyChanged(nameof(NameError));
        }
    }

    private void ValidateName()
    {
        this.ShowNameError = string.IsNullOrEmpty(Name);
    }
    #endregion

    #region Email
    private bool showEmailError;

    public bool ShowEmailError
    {
        get => showEmailError;
        set
        {
            showEmailError = value;
            OnPropertyChanged(nameof(ShowEmailError));
        }
    }

    private string email;
    public string Email
    {
        get => email;
        set
        {
            email = value;
            ValidateEmail();
            OnPropertyChanged(nameof(Email));
        }
    }

    private string emailError;

    public string EmailError
    {
        get => emailError;
        set
        {
            emailError = value;
            OnPropertyChanged(nameof(EmailError));
        }
    }

    private void ValidateEmail()
    {
        this.ShowEmailError = string.IsNullOrEmpty(Email);
        if (!ShowEmailError)
        {
            //check if email is in the correct format using regular expression
            if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                EmailError = "Email is not valid";
                ShowEmailError = true;
            }
            else
            {
                EmailError = "";
                ShowEmailError = false;
            }
        }
        else
        {
            EmailError = "Email is required";
        }
    }
    #endregion



    #region Password
    private bool showPasswordError;

    public bool ShowPasswordError
    {
        get => showPasswordError;
        set
        {
            showPasswordError = value;
            OnPropertyChanged(nameof(ShowPasswordError));
        }
    }

    private string password;
    public string Password
    {
        get => password;
        set
        {
            password = value;
            ValidatePassword();
            OnPropertyChanged(nameof(Password));
        }
    }

    private string passwordError;

    public string PasswordError
    {
        get => passwordError;
        set
        {
            passwordError = value;
            OnPropertyChanged(nameof(PasswordError));
        }
    }

    private void ValidatePassword()
    {
        //Password must include characters and numbers and be longer than 4 characters
        if (string.IsNullOrEmpty(password) ||
            password.Length < 4 ||
            !password.Any(char.IsDigit) ||
            !password.Any(char.IsLetter))
        {
            this.ShowPasswordError = true;
        }
        else
            this.ShowPasswordError = false;
    }

    //This property will indicate if the password entry is a password
    private bool isPassword = true;
    public bool IsPassword
    {
        get => isPassword;
        set
        {
            isPassword = value;
            OnPropertyChanged(nameof(IsPassword));
        }
    }
    //This command will trigger on pressing the password eye icon
    public Command ShowPasswordCommand { get; }
    //This method will be called when the password eye icon is pressed
    public void OnShowPassword()
    {
        //Toggle the password visibility
        IsPassword = !IsPassword;
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

    #region In it Fields with data
    //Define a method to initialize the fields with data

    private async void InItFieldsDataAsync()
    {
        Name = user.Username;
        Email = user.Email;
        Password = user.UserPassword;
        PhotoURL = proxy.GetImagesBaseAddress() + user.ProfileImagePath;
    }
    #endregion

    #region Change
    private bool change;

    public bool Change
    {
        get => change;
        set
        {
            change = value;
            OnPropertyChanged("Change");
        }
    }
    #endregion

    public Command EditCommand { get; }

    public void OnEdit()
    {
        Change = true;
    }

    //Define a command for the Save button
    public Command SaveCommand { get; }

    //Define a method that will be called when the register button is clicked
    public async void OnSave()
    {
        ValidateName();
        ValidateEmail();
        ValidatePassword();


        User currentUser = ((App)App.Current).LoggedInUser;
        if (!ShowNameError && !ShowEmailError && !ShowPasswordError)
        {
            //Update AppUser object with the data from the Edit form

            currentUser.Username = Name;
            currentUser.Email = Email;
            currentUser.UserPassword = Password;



            //Call the Register method on the proxy to register the new user
            InServerCall = true;
            bool success = await proxy.UpdateUser(currentUser);

            Change = false;
            //If the save was successful, navigate to the login page
            if (success)
            {

                //Upload profile image if needed
                if (!string.IsNullOrEmpty(LocalPhotoPath))
                {
                    User? updatedUser = await proxy.UploadProfileImage(LocalPhotoPath);
                    if (updatedUser == null)
                    {
                        await Shell.Current.DisplayAlert("Save Profile", "User Data Was Saved BUT Profile image upload failed", "ok");
                    }
                    else
                    {
                        UpdatePhotoURL(updatedUser.ProfileImagePath);
                    }

                }
                InServerCall = false;
                await Shell.Current.DisplayAlert("Save Profile", "Profile saved successfully", "ok");
                ((App)Application.Current).LoggedInUser.Username = Name;
                ((App)Application.Current).LoggedInUser.UserPassword = Password;
                ((App)Application.Current).LoggedInUser.Email = Email;
                ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<Login>());
            }
            else
            {
                InServerCall = false;
                //If the registration failed, display an error message
                string errorMsg = "Save Profile failed. Please try again.";
                await Shell.Current.DisplayAlert("Save Profile", errorMsg, "ok");
            }
        }
    }
}