using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ImageEditor.ViewModel;
using ImageEditor.Model;
using System.Windows.Media.Imaging;

namespace ImageEditor.Tools
{
    /// <summary>
    /// Brush Tool.
    /// </summary>
    public class BrushTool : ToolBase
    {
        public override void OnMouseMove(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
            if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {
                sender.EditedImage.CurrentLayer.Image = ImageHelper.CreateRenderTarget((int)sender.EditedImage.CurrentLayer.Image.Width, (int)sender.EditedImage.CurrentLayer.Image.Height,
                    (visual, context) =>
                    {
                        context.DrawImage(sender.EditedImage.CurrentLayer.Image, new Rect(0.0, 0.0, sender.EditedImage.CurrentLayer.Image.Width, sender.EditedImage.CurrentLayer.Image.Height));
                        if (e.LeftButton == MouseButtonState.Pressed)
                            // Painting with Foreground Color:
                            context.DrawLine(sender.Toolbox.ForePen, this.previousCoordinates, position);
                        else if (e.RightButton == MouseButtonState.Pressed)
                            // Painting with Background Color:
                            context.DrawLine(sender.Toolbox.BackPen, this.previousCoordinates, position);
                    });
            }
            // Saving current Position to draw next Segment from it:
            this.previousCoordinates = position;
        }
        
        #region Properties

        public override BitmapSource Icon
        {
            get { return new BitmapImage(new Uri("/ImageEditor;component/Images/brush.png", UriKind.Relative)); }
        }

        #endregion

        #region Field Declaration
        private Point previousCoordinates = default(Point);

        #endregion
    }
}
