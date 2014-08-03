using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;

namespace ImageEditor.ViewModel
{
    /// <summary>
    /// Provides Callbacks to allow Tools to interact with Canvas.
    /// </summary>
    public interface ICanvasCallback
    {
        void OnMouseDown(CanvasViewModel sender, MouseButtonEventArgs e, Point position);

        void OnMouseUp(CanvasViewModel sender, MouseButtonEventArgs e, Point position);

        void OnMouseMove(CanvasViewModel sender, MouseEventArgs e, Point position);
    }
}
