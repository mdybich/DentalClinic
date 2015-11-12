using System.ComponentModel;
using System.Windows;

namespace DentalClinic.UI.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void ShowWindow<T>() where T : Window, new()
        {
            var window = new T();
            window.Show();
        }
    }
}
