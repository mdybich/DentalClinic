using System.Windows;
using DentalClinic.UI.Interfaces;

namespace DentalClinic.UI.Views
{
    /// <summary>
    /// Interaction logic for AddVacationWindow.xaml
    /// </summary>
    public partial class AddVacationWindow : Window, IClosable
    {
        public AddVacationWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
    }
}
