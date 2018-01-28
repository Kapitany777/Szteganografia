using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Szakdolgozat.XMessageBox
{
    public static class ExtendedMessageBox
    {
        public static void ShowInfo(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Figyelmeztetés!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
