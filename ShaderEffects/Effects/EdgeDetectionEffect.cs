// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MDS.ShaderEffects {

    /// <summary>
    /// A shader effect for accentuating edges in a target texture
    /// </summary>
    public class EdgeDetectionEffect : ShaderEffect {

        #region Dependency Properties

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(EdgeDetectionEffect), 0);

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

        public EdgeDetectionEffect() {
            PixelShader = new PixelShader {
                UriSource = Global.MakePackUri("Shaders/EdgeDetection.ps")
            };
            DdxUvDdyUvRegisterIndex = 0;
            UpdateShaderValue(InputProperty);
        }

    }

}
