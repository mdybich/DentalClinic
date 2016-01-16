using DentalClinic.UI.Interfaces;
using System.Windows;

namespace DentalClinic.UI.Views
{
    /// <summary>
    /// Interaction logic for WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window, IClosable
    {
        public WorkerWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
    }
}
