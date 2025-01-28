using DutydoneClient.Services;

namespace DutydoneClient.Models;

public class User
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string UserPassword { get; set; }
    public bool IsAdmin { get; set; }
    public string ProfileImagePath { get; set; }
    public string FullImageUrl
    {

        get
        {
            return DutyDoneAPIProxy.BaseAddress+this.ProfileImagePath;
        }
    }
    public User() { }
    public User(Models.User user)
    {
        this.Email = Email;
        this.Username = Username;
        this.UserPassword = UserPassword;
        this.IsAdmin = user.IsAdmin;
    }
}
