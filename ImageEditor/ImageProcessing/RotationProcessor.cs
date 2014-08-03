using System;
using System.Collections.Generic;
using ImageEditor.Model;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageEditor.ImageProcessing
{
    /// <summary>
    /// Image Processor used for Rotation.
    /// </summary>
    public class RotationProcessor : ImageProcessorBase
    {
        public override void Process(EditedImage image)
        {
            RotationSettings settings = new RotationSettings();
            if (settings.ShowDialog() == true)
            {
                // Selecting rotation Angle:
                RotateTransform rotation = new RotateTransform();
                if (settings.rotate90Right.IsChecked == true)
                    rotation.Angle = 90;
                else if (settings.rotate180.IsChecked == true)
                    rotation.Angle = 180;
                else if (settings.rotate90Left.IsChecked == true)
                    rotation.Angle = 270;
                else
                    return;
                // Computing Rotation Center:
                rotation.CenterX = image.Width / 2;
                rotation.CenterY = image.Height / 2;
                // Rotating current Layer's Image:
                image.CurrentLayer.Image=ImageHelper.CreateRenderTarget((int)image.CurrentLayer.Image.Width, (int)image.CurrentLayer.Image.Height,
                    (visual, context) =>
                    {
                        context.DrawImage(image.CurrentLayer.Image, new Rect(0.0, 0.0, image.CurrentLayer.Image.Width, image.CurrentLayer.Image.Height));
                        visual.Transform = rotation;
                    });
            }
        }

        public override string Name
        {
            get { return StringResources.RotationProcessor; }
        }
    }
}
