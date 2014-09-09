// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MDS.ShaderEffects {

    /// <summary>
    /// A shader effect that adjusts the brightness and contrast of a target texture
    /// </summary>
    public class AdjustBrightnessContrastEffect : ShaderEffect {

        #region Dependency Properties

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(AdjustBrightnessContrastEffect), 0);

        public static readonly DependencyProperty ContrastProperty =
            DependencyProperty.Register("Contrast", typeof(double), typeof(AdjustBrightnessContrastEffect),
                    new UIPropertyMetadata(0.5, PixelShaderConstantCallback(0)), OnValidateBrightnessContrast);

        public static readonly DependencyProperty BrightnessProperty =
            DependencyProperty.Register("Brightness", typeof(double), typeof(AdjustBrightnessContrastEffect),
                    new UIPropertyMetadata(0.5, PixelShaderConstantCallback(1)), OnValidateBrightnessContrast);

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

        /// <summary>
        /// Contrast change between -1.0 and 1.0
        /// </summary>
        public double Contrast {
            get {
                return (double)GetValue(ContrastProperty);
            }
            set {
                SetValue(ContrastProperty, value);
            }
        }

        /// <summary>
        /// Brightness change between -1.0 and 1.0
        /// </summary>
        public double Brightness {
            get {
                return (double)GetValue(BrightnessProperty);
            }
            set {
                SetValue(BrightnessProperty, value);
            }
        }

        #endregion

        private static bool OnValidateBrightnessContrast(object value) {
            if (value is double) {
                double dv = (double)value;
                return dv >= -1.0 && dv <= 1.0;
            }
            return false;
        }

        public AdjustBrightnessContrastEffect() {
            PixelShader = new PixelShader {
                UriSource = Global.MakePackUri("Shaders/Adjust/BrightnessContrast.ps")
            };
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(ContrastProperty);
            UpdateShaderValue(BrightnessProperty);
        }

    }

}
