using DentalClinic.UI.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace DentalClinic.UI.Views
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window, IClosable
    {
        public AddUserWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }

        private void PasswordConfirmationBox_PasswordConfirmationChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).PasswordConfirmation = ((PasswordBox)sender).Password;
            }
        }
    }
}
