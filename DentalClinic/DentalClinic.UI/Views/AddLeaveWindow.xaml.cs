using System.Windows;
using DentalClinic.UI.Interfaces;

namespace DentalClinic.UI.Views
{
    /// <summary>
    /// Interaction logic for AddLeaveWindow.xaml
    /// </summary>
    public partial class AddLeaveWindow : Window, IClosable
    {
        public AddLeaveWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
    }
}
