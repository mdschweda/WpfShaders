// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MDS.ShaderEffects {

    /// <summary>
    /// A shader effect that adjusts the hue of a target texture
    /// </summary>
    public class AdjustHueEffect : ShaderEffect {

        #region Dependency Properties

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(AdjustHueEffect), 0);

        public static readonly DependencyProperty DeltaProperty =
            DependencyProperty.Register("Delta", typeof(double), typeof(AdjustHueEffect),
                    new UIPropertyMetadata(0.5, PixelShaderConstantCallback(0)), OnValidateDelta);

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
        /// Hue change between -1.0 and 1.0
        /// </summary>
        public double Delta {
            get {
                return (double)GetValue(DeltaProperty);
            }
            set {
                SetValue(DeltaProperty, value);
            }
        }

        #endregion

        private static bool OnValidateDelta(object value) {
            if (value is double) {
                double n = (double)value;
                return n >= -1.0 && n <= 1.0;
            }
            return false;
        }

        public AdjustHueEffect() {
            PixelShader = new PixelShader {
                UriSource = Global.MakePackUri("Shaders/Adjust/Hue.ps")
            };
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(DeltaProperty);
        }

    }

}
