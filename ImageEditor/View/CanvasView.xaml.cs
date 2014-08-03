using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using ImageEditor.ViewModel;

namespace ImageEditor.View
{
    /// <summary>
    /// Interaction logic for CanvasView.xaml
    /// </summary>
    public partial class CanvasView : UserControl
    {
        public CanvasView()
        {
            InitializeComponent();
            // ScrollViewer blocks some Mouse Events, so adding Handlers for the whole Canvas manually:
            base.AddHandler(Mouse.MouseDownEvent, new MouseButtonEventHandler(this.editedImage_MouseDown), true);
            base.AddHandler(Mouse.MouseUpEvent, new MouseButtonEventHandler(this.editedImage_MouseUp), true);
            base.AddHandler(Mouse.MouseMoveEvent, new MouseEventHandler(this.editedImage_MouseMove), true);
        }
        
        private void editedImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CanvasViewModel viewModel = base.DataContext as CanvasViewModel;
            if (viewModel != null)
                viewModel.OnMouseDown(this, e, e.GetPosition(this.editedImage));
        }

        private void editedImage_MouseMove(object sender, MouseEventArgs e)
        {
            CanvasViewModel viewModel = base.DataContext as CanvasViewModel;
            if (viewModel != null)
                viewModel.OnMouseMove(this, e, e.GetPosition(this.editedImage));
        }

        private void editedImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CanvasViewModel viewModel = base.DataContext as CanvasViewModel;
            if (viewModel != null)
                viewModel.OnMouseUp(this, e, e.GetPosition(this.editedImage));
        }
    }
}
