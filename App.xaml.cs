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
        public App(IServiceProvider serviceProvider, DutyDoneAPIProxy proxy)
        {
            this.proxy = proxy;
            InitializeComponent();
            LoggedInUser = null;
            
            //Start with the Login View
            MainPage = new NavigationPage(serviceProvider.GetService<Register>());
        }
    }
}
