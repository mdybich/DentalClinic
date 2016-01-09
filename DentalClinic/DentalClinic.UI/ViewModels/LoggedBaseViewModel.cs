using DentalClinic.UI.Authorization;
using DentalClinic.UI.Interfaces;
using DentalClinic.UI.Views;
using GalaSoft.MvvmLight.CommandWpf;
using System.Threading;

namespace DentalClinic.UI.ViewModels
{
    public abstract class LoggedBaseViewModel : BaseViewModel
    {
        public string DisplayName { get; set; }
        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }
        public RelayCommand<IClosable> LogoutCommand { get; set; }
        protected LoggedBaseViewModel()
        {
            var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            DisplayName = string.Format("{0} {1}", customPrincipal.Identity.FirstName, customPrincipal.Identity.LastName);

            LogoutCommand = new RelayCommand<IClosable>(LogoutExecute, CanLogout);
        }

        private void LogoutExecute(IClosable window)
        {
            var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;

            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
            }

            ShowWindow<LoginWindow>();

            if (window != null)
            {
                window.Close();
            }           
        }

        private bool CanLogout(IClosable arg)
        {
            return IsAuthenticated;
        }
    }
}
