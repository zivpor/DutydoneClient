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
            Routing.RegisterRoute("Profile",typeof(ProfilePage));
            Routing.RegisterRoute("addPeople", typeof(AddPeople));
        }

        public event Action<Type> DataChanged;
        public void Refresh(Type type)
        {
            if (DataChanged != null)
            {
                DataChanged(type);
            }
        }
    }
}
