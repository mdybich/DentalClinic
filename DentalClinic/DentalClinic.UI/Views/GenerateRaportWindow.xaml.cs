using DentalClinic.UI.Interfaces;
using System.Windows;

namespace DentalClinic.UI.Views
{
    /// <summary>
    /// Interaction logic for GenerateRaportWindow.xaml
    /// </summary>
    public partial class GenerateRaportWindow : Window, IClosable
    {
        public GenerateRaportWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
    }
}
