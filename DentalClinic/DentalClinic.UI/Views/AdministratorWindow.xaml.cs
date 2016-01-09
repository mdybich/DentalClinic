using DentalClinic.UI.Interfaces;
using System.Windows;

namespace DentalClinic.UI.Views
{
    /// <summary>
    /// Interaction logic for AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window, IClosable
    {
        public AdministratorWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
    }
}
