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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using ImageEditor.ViewModel;
using ImageEditor.Tools;
using ImageEditor.Effects;
using ImageEditor.ImageProcessing;

namespace ImageEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            base.DataContext = new AppViewModel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !this.ViewModel.BeforeClosing();
        }
        
        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.New();
            e.Handled = true;
        }
        
        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.Open();
            e.Handled = true;
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.Save();
            e.Handled = true;
        }


        private void SaveAsExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.SaveAs();
            e.Handled = true;
        }

        private void ProcessImageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.ProcessImage(e.Parameter as ImageProcessorBase);
            e.Handled = true;
        }

        private void LayersPaletteExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.UpdateLayersPalette();
            e.Handled = true;
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.ViewModel.Close(e.Parameter as CanvasViewModel);
            e.Handled = true;
        }

        private void AnyImageExists(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ViewModel.AnyImageExists();
            e.Handled = true;
        }

        #region Properties

        private AppViewModel ViewModel
        {
            get { return (AppViewModel)base.DataContext; }
        }

        #endregion
    }
}
