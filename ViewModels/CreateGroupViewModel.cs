using DutydoneClient.Services;
using DutydoneClient.Models;
using System.Text.RegularExpressions;


namespace DutydoneClient.ViewModels;

public class CreateGroupViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;
    public CreateGroupViewModel(DutyDoneAPIProxy proxy)
    {
        this.proxy = proxy;
        CreateCommand = new Command(Create);
        NameError = "Name is required";
        TypeError = "Type is required";
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

    private string type;

    public string Type
    {
        get => type;
        set
        {
            type = value;
            ValidateType();
            OnPropertyChanged("Type");
        }
    }

    private string typeError;

    public string TypeError
    {
        get => typeError;
        set
        {
            typeError = value;
            OnPropertyChanged("TypeError");
        }
    }

    private void ValidateType()
    {
        this.ShowTypeError = string.IsNullOrEmpty(Type);
    }
    #endregion
    public async void Create()
    {
        ValidateName();

        ValidateType();


        if (!ShowNameError && !ShowTypeError)
        {
           
            var newGroup = new Models.Group()
            {
                GroupName = Name,
                GroupType = Type


            };

            //Call the Register method on the proxy to register the new user
            InServerCall = true;
            int? groupId = await proxy.CreateGroup(newGroup);
            InServerCall = false;

            //If the registration was successful, navigate to the login page
            if (groupId != null)
            {

                InServerCall = false;
                newGroup.groupId = newGroup.Value;
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