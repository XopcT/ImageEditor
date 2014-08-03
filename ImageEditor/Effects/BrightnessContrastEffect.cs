using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ImageEditor.Effects
{
    public class BrightnessContrastEffect : ShaderEffect
    {
        private static PixelShader _pixelShader = new PixelShader() { UriSource = new Uri(@"pack://application:,,,/Effects/BrightnessContrast.ps") };

        public BrightnessContrastEffect()
        {
            PixelShader = _pixelShader;

            base.UpdateShaderValue(inputProperty);
            base.UpdateShaderValue(brightnessProperty);
            base.UpdateShaderValue(contrastProperty);
        }


        private static object CoerceBrightness(DependencyObject d, object value)
        {
            BrightnessContrastEffect effect = (BrightnessContrastEffect)d;
            float newValue = (float)value;
            if (newValue < -1.0f || newValue > 1.0f)
                return effect.Brightness;
            return newValue;
        }

        private static object CoerceContrast(DependencyObject d, object value)
        {
            BrightnessContrastEffect effect = (BrightnessContrastEffect)d;
            float newValue = (float)value;
            if (newValue < 0.0f || newValue > 2.0f)
                return effect.Contrast;
            return newValue;
        }

        #region Properties
        public Brush Input
        {
            get { return (Brush)GetValue(inputProperty); }
            set { SetValue(inputProperty, value); }
        }

        public float Brightness
        {
            get { return (float)base.GetValue(brightnessProperty); }
            set { base.SetValue(brightnessProperty, value); }
        }

        public float Contrast
        {
            get { return (float)base.GetValue(contrastProperty); }
            set { base.SetValue(contrastProperty, value); }
        }

        #endregion

        #region Field Declaration
        private static readonly DependencyProperty inputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(BrightnessContrastEffect), 0);
        private static readonly DependencyProperty brightnessProperty = DependencyProperty.Register("Brightness", typeof(float), typeof(BrightnessContrastEffect), new UIPropertyMetadata(0.0f, PixelShaderConstantCallback(0), CoerceBrightness));
        private static readonly DependencyProperty contrastProperty = DependencyProperty.Register("Contrast", typeof(float), typeof(BrightnessContrastEffect), new UIPropertyMetadata(0.0f, PixelShaderConstantCallback(1), CoerceContrast));

        #endregion
    }
}
