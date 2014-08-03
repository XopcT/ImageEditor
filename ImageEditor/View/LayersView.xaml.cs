using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ImageEditor.ViewModel;
using ImageEditor.Model;

namespace ImageEditor.View
{
    /// <summary>
    /// Interaction logic for LayersView.xaml
    /// </summary>
    public partial class LayersView : Window
    {
        public LayersView()
        {
            InitializeComponent();
        }

        private void AddLayerExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.CurrentCanvas.EditedImage.AddLayer();
            e.Handled = true;
        }

        private void AddLayerCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CurrentCanvas != null && this.CurrentCanvas.EditedImage != null;
            e.Handled = true;
        }

        private void RemoveLayerExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.CurrentCanvas.EditedImage.RemoveLayer();
            e.Handled = true;
        }

        private void RemoveLayerCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CurrentCanvas != null && this.CurrentCanvas.CanRemoveLayer();
            e.Handled = true;
        }

        private void MoveLayerUpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.CurrentCanvas.MoveLayerUp();
            e.Handled = true;
        }

        private void MoveLayerUpCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CurrentCanvas != null && this.CurrentCanvas.CanMoveLayerUp();
            e.Handled = true;
        }

        private void MoveLayerDownExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.CurrentCanvas.MoveLayerDown();
            e.Handled = true;
        }

        private void MoveLayerDownCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CurrentCanvas != null && this.CurrentCanvas.CanMoveLayerDown();
            e.Handled = true;
        }

        #region Properties

        private CanvasViewModel CurrentCanvas
        {
            get
            {
                AppViewModel viewModel = base.DataContext as AppViewModel;
                if (viewModel != null)
                    return viewModel.CurrentCanvas;
                else
                    return null;
            }
        }

        #endregion

        #region Field Declaration
        
        #endregion

    }
}
