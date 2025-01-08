using DutydoneClient.ViewModels;

namespace DutydoneClient.Views;

public partial class Groups : ContentPage
{
    bool firstTime;
	public Groups(GroupsViewModel vm)
	{
        firstTime = true;
        this.BindingContext = vm;
        InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
    
        

        if (firstTime)
            firstTime = false;
        else
        {
            GroupsViewModel vm = (GroupsViewModel)this.BindingContext;
            vm.Refresh();
        }
        
    }
}