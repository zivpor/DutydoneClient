using DutydoneClient.ViewModels;

namespace DutydoneClient
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}
