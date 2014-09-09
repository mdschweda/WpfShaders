// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MDS.ShaderEffects {

    /// <summary>
    /// A shader effect for solarization of a target texture
    /// </summary>
    public class SolarizeEffect : ShaderEffect {

        #region Dependency Properties

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(SolarizeEffect), 0);

        public static readonly DependencyProperty ThresholdProperty =
            DependencyProperty.Register("Threshold", typeof(double), typeof(SolarizeEffect),
                    new UIPropertyMetadata(0.5, PixelShaderConstantCallback(0)), OnValidateThreshold);

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
        /// Threshold for inversion of color channels between 0 and 1.0
        /// </summary>
        public double Threshold {
            get {
                return (double)GetValue(ThresholdProperty);
            }
            set {
                SetValue(ThresholdProperty, value);
            }
        }

        #endregion

        private static bool OnValidateThreshold(object value) {
            if (value is double) {
                double threshold = (double)value;
                return threshold >= 0 && threshold < 1.0;
            }
            return false;
        }

        public SolarizeEffect() {
            PixelShader = new PixelShader {
                UriSource = Global.MakePackUri("Shaders/Solarize.ps")
            };
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(ThresholdProperty);
        }

    }

}
