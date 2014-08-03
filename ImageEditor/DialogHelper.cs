using System;
using System.Collections.Generic;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace ImageEditor
{
    /// <summary>
    /// Provides some Common Dialogs.
    /// </summary>
    public static class DialogHelper
    {
        /// <summary>
        /// Shows the open File Dialog to select an Image.
        /// </summary>
        /// <param name="file">Retrieves a File to open.</param>
        /// <returns>True if a File was selected, False otherwise.</returns>
        public static bool ShowOpenFileDialog(out FileInfo file)
        {
            // Initializing Dialog:
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = StringResources.OpenFileFilter;
            dialog.Multiselect = false;

            // Checking if a File was selected:
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                file = new FileInfo(dialog.FileName);
                return true;
            }
            else
            {
                file = null;
                return false;
            }
        }

        /// <summary>
        /// Shows the save File Dialog.
        /// </summary>
        /// <param name="file">Retrieves a File to save to.</param>
        /// <returns>True if a File was selected, False otherwise.</returns>
        public static bool ShowSaveFileDialog(out FileInfo file)
        {
            // Initializing Dialog:
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = StringResources.SaveFileFilter;
            dialog.AddExtension = true;

            // Checking if a File was selected:
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                file = new FileInfo(dialog.FileName);
                return true;
            }
            else
            {
                file = null;
                return false;
            }
        }

        /// <summary>
        /// Shows an Error Message.
        /// </summary>
        /// <param name="message">Message to show.</param>
        public static void ShowCriticalError(string message)
        {
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Shows a File Save Request.
        /// </summary>
        /// <param name="message">Message to show.</param>
        /// <returns>User's Choise.</returns>
        public static MessageBoxResult SaveRequest(string message)
        {
            return MessageBox.Show(message, string.Empty, MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
        }
    }
}
