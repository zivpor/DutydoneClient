using DutydoneClient.Models;
using DutydoneClient.Services;
using DutydoneClient.ViewModels;
using DutydoneClient.Views;

namespace DutydoneClient
{
    public partial class App : Application
    {
        public AppBasicData BasicData { get; set; }
        public User? LoggedInUser { get; set; }
        private DutyDoneAPIProxy proxy;
        public App(DutyDoneAPIProxy proxy, IServiceProvider serviceProvider)
        {
            
            this.proxy = proxy;
            ReadBasicDataFromServer();
            InitializeComponent();
            Login? v = serviceProvider.GetService<Login>();
            
            //Start with the Login View
            MainPage = new NavigationPage(v);
        }

        private async void ReadBasicDataFromServer()
        {
            this.BasicData = await proxy.GetBasicData();
        }

    }
}
