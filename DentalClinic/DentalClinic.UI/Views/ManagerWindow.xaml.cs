using DentalClinic.UI.Interfaces;
using System.Windows;

namespace DentalClinic.UI.Views
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window, IClosable
    {
        public ManagerWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
    }
}
