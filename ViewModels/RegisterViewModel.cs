namespace DutydoneClient.ViewModels;

public class RegisterViewModel : ViewModelBase
{
    private string name;
    private int? userId { get; set; }
    private string? user_error;
    private string email;
    private string password;
    private string? password_error;

    public RegisterViewModel()
    {

    }
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
            User_Error = ""; // ����� ����� �� ������
            OnPropertyChanged(nameof(Name));
            // ����� ������ �� ������

            if (!string.IsNullOrEmpty(Name))

            {
                if (char.IsDigit(Name[0]))
                {
                    User_Error = "!!�� ������ �� ���� ������ �����!!";
                    OnPropertyChanged(nameof(Name));
                }

            }
        }
    }
    public string User_Error
    {
        get
        {
            return user_error;
        }
        set
        {
            user_error = value;
            OnPropertyChanged(nameof(User_Error));
        }
    }
}