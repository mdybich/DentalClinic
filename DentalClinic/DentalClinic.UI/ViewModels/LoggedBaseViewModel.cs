using DentalClinic.UI.Authorization;
using System.Threading;

namespace DentalClinic.UI.ViewModels
{
    public abstract class LoggedBaseViewModel : BaseViewModel
    {
        protected LoggedBaseViewModel()
        {
            var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            DisplayName = string.Format("{0} {1}", customPrincipal.Identity.FirstName, customPrincipal.Identity.LastName);
        }

        public string DisplayName { get; set; }
    }
}
