using System;
using System.Collections.Generic;
using System.Windows.Media.Effects;
using ImageEditor.Effects;
using ImageEditor.Model;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ImageEditor.ImageProcessing
{
    /// <summary>
    /// Image Processor used to adjust Brightness/Contrast.
    /// </summary>
    public class BrightnessContrastProcessor : ImageProcessorBase
    {
        public override void Process(EditedImage image)
        {
            this.editedImage = image;
            this.original = this.editedImage.CurrentLayer.Image;
            this.Brightness = 0.0f;
            this.Contrast = 1.0f;
            // Showing Brightness/Contrast Settings:
            BrightnessContrastSettings settings = new BrightnessContrastSettings();
            settings.DataContext = this;
            if (settings.ShowDialog() == false)
                // Cancelling all the made Changes:
                this.editedImage.CurrentLayer.Image = this.original;
        }

        private void ApplyPreview()
        {
            this.editedImage.CurrentLayer.Image = ImageHelper.CreateRenderTarget(
                (int)this.editedImage.CurrentLayer.Image.Width, (int)this.editedImage.CurrentLayer.Image.Height,
                (visual, context) =>
                {
                    visual.Effect = this.effect.Value;
                    context.DrawImage(this.original, new Rect(0.0, 0.0, this.editedImage.CurrentLayer.Image.Width, this.editedImage.CurrentLayer.Image.Height));
                });
        }
        
        #region Properties

        public override string Name
        {
            get { return StringResources.BrightnessContrastProcessor; }
        }

        public float Brightness
        {
            get { return this.effect.Value.Brightness; }
            set
            {
                this.effect.Value.Brightness = value;
                this.ApplyPreview();
                base.OnPropertyChanged("Brightness");                
            }
        }

        public float Contrast
        {
            get { return this.effect.Value.Contrast; }
            set
            {
                this.effect.Value.Contrast = value;
                this.ApplyPreview();
                base.OnPropertyChanged("Contrast");
            }
        }

        #endregion

        #region Field Declaration
        private Lazy<BrightnessContrastEffect> effect = new Lazy<BrightnessContrastEffect>(() => new BrightnessContrastEffect());
        private EditedImage editedImage = null;
        private BitmapSource original = null;

        #endregion
    }
}
