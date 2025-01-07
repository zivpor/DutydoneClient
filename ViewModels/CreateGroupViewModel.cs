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
       
    }
    public Command CreateCommand { get; }
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
    private void ValidateName()
    {
        this.ShowNameError = string.IsNullOrEmpty(Name);
    }
    private bool groupNameError;
   
    public async void Create()
    {
        var newGroup = new Group
        {
            NameGroupName = Name,
           
           

        };

        //Call the Register method on the proxy to register the new user
        InServerCall = true;
        int? userId = await proxy.Register(newGroup);
        InServerCall = false;

        //If the registration was successful, navigate to the login page
        if (userId != null)
        {

            InServerCall = false;
            newUser.UserId = userId.Value;
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }
    }
}