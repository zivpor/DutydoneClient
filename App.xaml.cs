using DutydoneClient.ViewModels;
using DutydoneClient.Views;

namespace DutydoneClient
{
    public partial class App : Application
    {
        public App(RegisterViewModel vm)
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new Register(vm);
        }
    }
}
