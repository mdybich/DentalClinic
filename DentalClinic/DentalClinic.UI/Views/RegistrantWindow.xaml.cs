using DentalClinic.UI.Interfaces;
using System.Windows;

namespace DentalClinic.UI.Views
{
    /// <summary>
    /// Interaction logic for RegistrantWindow.xaml
    /// </summary>
    public partial class RegistrantWindow : Window, IClosable
    {
        public RegistrantWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
    }
}
