using DutydoneClient.Models;
using DutydoneClient.Services;
using System.Collections.ObjectModel;


namespace DutydoneClient.ViewModels;

public class AddTaskViewModel : ViewModelBase
{
    private DutyDoneAPIProxy proxy;
    public ObservableCollection<TaskType> TaskTypes { get; set; }
    public AddTaskViewModel(DutyDoneAPIProxy proxy)
	{
        this.TaskTypes = new ObservableCollection<TaskType>(((App)Application.Current).BasicData.TaskTypes);
        SelectedTaskType = this.TaskTypes[0];
        this.proxy = proxy;
        AddTaskCommand = new Command(AddTask);
        NameError = "Name is required";
    }
    public Command AddTaskCommand { get; }
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
    #region Task Type
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

    private GroupType selectedTaskType;

    public GroupType SelectedTaskType
    {
        get => selectedTaskType;
        set
        {
            selectedTaskType = value;
            OnPropertyChanged("SelectedTaskType");
        }
    }




    #endregion
}