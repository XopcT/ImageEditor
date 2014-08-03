using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace ImageEditor.ImageProcessing
{
    /// <summary>
    /// Interaction logic for SizeSettings.xaml
    /// </summary>
    public partial class SizeSettings : Window
    {
        public SizeSettings()
        {
            InitializeComponent();
        }

        private void editor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !this.ValidateText(e.Text);
        }

        private bool ValidateText(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            base.DialogResult = true;
            base.Close();
        }
    }
}
