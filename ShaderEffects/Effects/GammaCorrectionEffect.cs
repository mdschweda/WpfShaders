// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MDS.ShaderEffects {

    /// <summary>
    /// A shader effect that provides gamma correction
    /// </summary>
    public class GammaCorrectionEffect : ShaderEffect {

        #region Dependency Properties

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(GammaCorrectionEffect), 0);

        public static readonly DependencyProperty GammaProperty =
            DependencyProperty.Register("Gamma", typeof(double), typeof(GammaCorrectionEffect),
                    new UIPropertyMetadata(2.0, PixelShaderConstantCallback(0)), OnValidateGamma);

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
        /// A positive gamma value
        /// </summary>
        public double Gamma {
            get {
                return (double)GetValue(GammaProperty);
            }
            set {
                SetValue(GammaProperty, value);
            }
        }

        #endregion

        private static bool OnValidateGamma(object value) {
            return value is double && (double) value >= 0;
        }

        public GammaCorrectionEffect() {
            PixelShader = new PixelShader {
                UriSource = Global.MakePackUri("Shaders/GammaCorrection.ps")
            };
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(GammaProperty);
        }

    }

}
