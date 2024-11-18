namespace DutydoneClient.Models;

public class User : ContentPage
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string UserPassword { get; set; }
    public User() { }
    public User(Models.User user)
    {
        this.Email = Email;
        this.Username = Username;
        this.UserPassword = UserPassword;
    }
}