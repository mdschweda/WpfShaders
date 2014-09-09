// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MDS.ShaderEffects {

    /// <summary>
    /// A shader effect that applies a Gaussian blur on a target texture
    /// </summary>
    public class GaussianBlurEffect : ShaderEffect {

        #region Dependency Properties

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(GaussianBlurEffect), 0);

        public static readonly DependencyProperty SigmaProperty =
            DependencyProperty.Register("Sigma", typeof(double), typeof(GaussianBlurEffect),
                    new UIPropertyMetadata(1.0, PixelShaderConstantCallback(0)), OnValidateSigma);

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
        /// A positive sigma value, affecting the blur strength
        /// </summary>
        public double Sigma {
            get {
                return (double)GetValue(SigmaProperty);
            }
            set {
                SetValue(SigmaProperty, value);
            }
        }

        #endregion

        private static bool OnValidateSigma(object value) {
            if (value is double)
                return (double)value >= 0.0;
            return false;
        }

        public GaussianBlurEffect() {
            PixelShader = new PixelShader {
                UriSource = Global.MakePackUri("Shaders/GaussianBlur.ps")
            };
            DdxUvDdyUvRegisterIndex = 1;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(SigmaProperty);
        }

    }

}
