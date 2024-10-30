using DutydoneClient.ViewModels;
using DutydoneClient.Views;

namespace DutydoneClient
{
    public partial class App : Application
    {
        public App(LoginViewModel vm)
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new Login(vm);
        }
    }
}
