using DutydoneClient.ViewModels;
using DutydoneClient.Views;

namespace DutydoneClient
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
            RegisterRoutes();
        }

        
        private void RegisterRoutes()
        {
            Routing.RegisterRoute("createGroup", typeof(CreateGroup));
            Routing.RegisterRoute("groupPage", typeof(GroupPage));
            Routing.RegisterRoute("manageredGroupPage", typeof(ManageredGroupPage));
            Routing.RegisterRoute("addTask", typeof(AddTask));
            Routing.RegisterRoute("editProfile",typeof(EditProfile));
        }
    }
}
