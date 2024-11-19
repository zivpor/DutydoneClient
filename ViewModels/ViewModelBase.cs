using System.ComponentModel;

namespace DutydoneClient.ViewModels;

public class ViewModelBase : INotifyPropertyChanged
{

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
    private bool inServerCall;
    public bool InServerCall
    {
        get
        {
            return this.inServerCall;
        }
        set
        {
            this.inServerCall = value;
            OnPropertyChanged("NotInServerCall");
            OnPropertyChanged("InServerCall");
        }
    }

    public bool NotInServerCall
    {
        get
        {
            return !this.InServerCall;
        }
    }
}