using DutydoneClient.Services;

namespace DutydoneClient.ViewModels;

public class CreateGroupViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;
    public CreateGroupViewModel(DutyDoneAPIProxy proxy)
	{
        this.proxy = proxy;
        CreateCommand = new Command(Create);
        GroupNameError = "Name is required";
    }
    public Command CreateCommand { get; }
    public async void Create()
    {

    }
}