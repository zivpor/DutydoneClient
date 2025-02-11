using DutydoneClient.Models;
using DutydoneClient.Services;
using System.Collections.ObjectModel;


namespace DutydoneClient.ViewModels;
[QueryProperty("Group", "group")]
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
        TaskActualDate = DateTime.Now;
        
    }

    private Group group;
    public Group Group
    {
        get => group;
        set
        {
            if (group != value)
            {
                group = value;
                OnPropertyChanged(nameof(Group));
            }
        }
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

    private TaskType selectedTaskType;

    public TaskType SelectedTaskType
    {
        get => selectedTaskType;
        set
        {
            selectedTaskType = value;
            OnPropertyChanged("SelectedTaskType");
        }
    }




    #endregion
    #region TaskDueDate
    private bool showTaskDueDateError;
    public bool ShowTaskDueDateError
    {
        get => showTaskDueDateError;
        set
        {
            showTaskDueDateError = value;
            OnPropertyChanged(nameof(ShowTaskDueDateError));
        }
    }
    private DateOnly taskDueDate;
    public DateTime TaskDueDate
    {
        get => taskDueDate.ToDateTime(TimeOnly.MinValue);
        set
        {
            taskDueDate = new DateOnly(value.Year, value.Month, value.Day);
            ValidateTaskDueDate();
            OnPropertyChanged(nameof(TaskDueDate));
        }
    }

    private string taskDueDateError;
    public string TaskDueDateError
    {

        get => taskDueDateError;
        set
        {
            taskDueDateError = value;
            OnPropertyChanged(nameof(TaskDueDateError));
        }
    }
    public void ValidateTaskDueDate()
    {
        this.ShowTaskDueDateError = taskDueDate < DateOnly.FromDateTime(DateTime.Now);
    }
    #endregion
    #region TaskActualDate
    private bool showTaskActualDateError;
    public bool ShowTaskActualDateError
    {
        get => showTaskActualDateError;
        set
        {
            showTaskActualDateError = value;
            OnPropertyChanged(nameof(ShowTaskActualDateError));
        }
    }
    private DateOnly? taskActualDate;
    public DateTime? TaskActualDate
    {
        get
        {
            if (taskActualDate == null)
                return null;
            else
            {
                DateOnly val = taskActualDate.Value;
                return val.ToDateTime(TimeOnly.MinValue);
            }

        }
        set
        {
            if (value == null)
                taskActualDate = null;
            else
            {
                DateTime val = value.Value;
                taskActualDate = new DateOnly(val.Year, val.Month, val.Day);
            }
            ValidateTaskActualDate();
            OnPropertyChanged(nameof(TaskActualDate));
        }
    }
    private string taskActualDateError;
    public string TaskActualDateError
    {
        get => taskActualDateError;
        set
        {
            taskActualDateError = value;
            OnPropertyChanged(nameof(TaskActualDateError));
        }
    }
    public void ValidateTaskActualDate()
    {
        this.ShowTaskActualDateError = false;

    }

    private bool taskDone;
    public bool TaskDone
    {
        get => taskDone;
        set
        {
            taskDone = value;
            if (!value)
            {
                TaskActualDate = null;
                TaskDoneText = "Not Done Yet";
            }
            else
            {
                TaskActualDate = (TaskActualDate == null ? DateTime.Now : TaskActualDate);
                TaskDoneText = $"Done At {taskActualDate}";
            }
            OnPropertyChanged(nameof(TaskDone));
        }
    }

    private string taskDoneText;
    public string TaskDoneText
    {
        get => taskDoneText;
        set
        {
            taskDoneText = value;
            OnPropertyChanged(nameof(TaskDoneText));
        }
    }


    #endregion
    public async void AddTask()
    {
        ValidateName();




        if (!ShowNameError)
        {

            var newTask = new Models.Task()
            {
                TaskName = Name,
                TaskType = SelectedTaskType.TaskTypeId,
                UserId = ((App)Application.Current).LoggedInUser.UserId,
                DueDay = taskActualDate,
                StatusId = 1,
                GroupId = Group.GroupId

            };

            //Call the Register method on the proxy to register the new user
            InServerCall = true;
            int? taskId = await proxy.AddTask(newTask);
            InServerCall = false;

            //If the registration was successful, navigate to the login page
            if (taskId != null)
            {

                InServerCall = false;
                newTask.TaskId = taskId.Value;
                ((App)(Application.Current)).MainPage.Navigation.PopAsync();
            }
            else
            {

                //If the registration failed, display an error message
                string errorMsg = "Adding Task failed. Please try again.";
                await Application.Current.MainPage.DisplayAlert("Adding Task", errorMsg, "ok");
            }


        }
    }
}