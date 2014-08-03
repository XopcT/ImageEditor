using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ImageEditor.ViewModel;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ImageEditor
{
    /// <summary>
    /// Base Class for creating Editor's Tools.
    /// </summary>
    public abstract class ToolBase : BindableObjectBase, ICanvasCallback
    {
        public virtual void OnMouseDown(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            // Nothing needs to be done in current Context.
        }

        public virtual void OnMouseUp(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            // Nothing needs to be done in current Context.
        }

        public virtual void OnMouseMove(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
            // Nothing needs to be done in current Context.
        }

        /// <summary>
        /// Retrieves the Tool Icon.
        /// </summary>
        public virtual BitmapSource Icon
        {
            get { return null; }
        }
    }
}
