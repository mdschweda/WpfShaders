using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace WpfShaderEffects {

    /// <summary>
    /// A shader effect that turns a target texture into a grayscale output
    /// </summary>
    public class GrayscaleEffect : ShaderEffect {

        #region Dependency Properties

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(GrayscaleEffect), 0);

        /// <summary>
        /// Brush that acts as the input
        /// </summary>
        public Brush Input {
            get {
                return (Brush)GetValue(InputProperty);
            }
            set {
                SetValue(InputProperty, value);
            }
        }

        #endregion

        private static bool OnValidateGamma(object value) {
            return value is double && (double) value >= 0;
        }

        public GrayscaleEffect() {
            PixelShader = new PixelShader {
                UriSource = Global.MakePackUri("Shaders/Grayscale.ps")
            };
            UpdateShaderValue(InputProperty);
        }

    }

}
