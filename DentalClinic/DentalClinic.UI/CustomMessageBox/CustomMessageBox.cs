using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DentalClinic.UI.CustomMessageBox
{
    public static class CustomMessageBox
    {
        public static MessageBoxResult Show (string message)
        {
            return Show(message, string.Empty);
        }

        public static MessageBoxResult Show(string message, string caption)
        {
            if (message.Length > 35)
            {
                var allWords = message.Split(' ');

                var newMessage = string.Empty;
                int i = 1;

                foreach (var word in allWords)
                {
                    newMessage += word + " ";
                    if (newMessage.Length > 30 * i)
                    {
                        newMessage += Environment.NewLine;
                        i++;
                    }
                }
                message = newMessage;
            }

            var dialog = new CustomMessageBoxView();
            dialog.Title = caption;
            dialog.tbMessage.Content = message;
            dialog.Caption.Content = caption;

            dialog.ShowDialog();

            return MessageBoxResult.OK;

        }
    }
}
