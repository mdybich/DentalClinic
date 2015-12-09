using GalaSoft.MvvmLight;
using System.Windows;

namespace DentalClinic.UI.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {

        protected void ShowWindow<T>() where T : Window, new()
        {
            var window = new T();
            window.Show();
        }
    }
}
