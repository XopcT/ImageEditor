using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ImageEditor.ViewModel;
using System.Windows.Media.Imaging;

namespace ImageEditor.Tools
{
    /// <summary>
    /// Base Class for Creating Tools drawing some Primitives.
    /// </summary>
    public abstract class PrimitiveTool : ToolBase
    {
        public override void OnMouseDown(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            // Starting drawing a Primitive:
            this.original = sender.EditedImage.CurrentLayer.Image;
            this.start = position;
        }

        public override void OnMouseMove(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
            // Checking if Drawing is in Progress:
            if (this.start.HasValue)
            {
                // Drawing the Primitive over the Original Image:
                sender.EditedImage.CurrentLayer.Image = ImageHelper.CreateRenderTarget(
                    (int)sender.EditedImage.CurrentLayer.Image.Width, (int)sender.EditedImage.CurrentLayer.Image.Height,
                    (visual, context) =>
                    {
                        // Drawing Original Image:
                        context.DrawImage(this.original, new Rect(0.0, 0.0, sender.EditedImage.CurrentLayer.Image.Width, sender.EditedImage.CurrentLayer.Image.Height));
                        // Drawing Primitive:
                        this.DrawMethod(context, sender, e, position);
                    });
            }
        }

        public override void OnMouseUp(ViewModel.CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            // Stop drawing a Primitive:
            this.start = null;
        }

        /// <summary>
        /// Method used to prepare for Drawing.
        /// </summary>
        private void DrawMethod(DrawingContext context, CanvasViewModel sender, MouseEventArgs e, Point position)
        {
            Brush brush = null;
            Pen pen = null;

            // Selecting a Brush and a Pen to draw depending on Mouse Button:
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                brush = sender.Toolbox.BackBrush;
                pen = sender.Toolbox.ForePen;
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                brush = sender.Toolbox.ForeBrush;
                pen = sender.Toolbox.BackPen;
            }
            // Drawing the Primitive:
            this.DrawMethod(context, this.start.Value, position, brush, pen);
        }

        /// <summary>
        /// Method used to directly Draw the Primitive. Must be overriden in inherited Tools.
        /// </summary>
        protected abstract void DrawMethod(DrawingContext context, Point start, Point end, Brush brush, Pen pen);
        
        #region Properties

        #endregion

        #region Field Declaration
        private BitmapSource original = null;
        private Point? start = null;

        #endregion
    }
}
