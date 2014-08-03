using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ImageEditor.Effects
{
    public class NegativeEffect : ShaderEffect
    {
        private static PixelShader _pixelShader = new PixelShader() { UriSource = new Uri(@"pack://application:,,,/Effects/Negative.ps") };

        public NegativeEffect()
        {
            PixelShader = _pixelShader;

            UpdateShaderValue(InputProperty);
        }

        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(NegativeEffect), 0);
        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }
    }
}
