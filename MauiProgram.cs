using DutydoneClient.Services;
using DutydoneClient.ViewModels;
using DutydoneClient.Views;
using Microsoft.Extensions.Logging;

namespace DutydoneClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Alloha.otf", "Alloha");
                    fonts.AddFont("yarden-regular-alefalefalef.oft", "Yarden");
                    fonts.AddFont("Super Magnum.otf", "SM");
                });
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<Register>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddSingleton<DutyDoneAPIProxy>();
            builder.Services.AddTransient<Groups>();
            builder.Services.AddTransient<GroupsViewModel>();
            builder.Services.AddTransient<CreateGroup>();
            builder.Services.AddTransient<CreateGroupViewModel>();
            builder.Services.AddTransient<GroupPage>();
            builder.Services.AddTransient <GroupPageViewModel>();
            builder.Services.AddTransient<ManageredGroupPage>();
            builder.Services.AddTransient<ManageredGroupPageViewModel>();
            builder.Services.AddTransient<AddTask>();
            builder.Services.AddTransient<AddTaskViewModel>();
            builder.Services.AddTransient<TasksList>();
            builder.Services.AddTransient<TasksListViewModel>();
            builder.Services.AddTransient<AdminPage>();
            builder.Services.AddTransient<AdminPageViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
