using DutydoneClient.Models;
using DutydoneClient.Services;
using DutydoneClient.ViewModels;
using DutydoneClient.Views;

namespace DutydoneClient
{
    public partial class App : Application
    {
        public User? LoggedInUser { get; set; }
        private DutyDoneAPIProxy proxy;
        public App(IServiceProvider serviceProvider)
        {
            
            InitializeComponent();
            Login? v = serviceProvider.GetService<Login>();
            
            //Start with the Login View
            MainPage = new NavigationPage(v);
        }
    }
}
