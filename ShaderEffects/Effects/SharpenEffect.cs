// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MDS.ShaderEffects {

    /// <summary>
    /// A shader effect that applies an unsharp mask on a target texture
    /// </summary>
    public class SharpenEffect : ShaderEffect {

        #region Dependency Properties

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(SharpenEffect), 0);

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

        public SharpenEffect() {
            PixelShader = new PixelShader {
                UriSource = Global.MakePackUri("Shaders/Sharpen.ps")
            };
            DdxUvDdyUvRegisterIndex = 0;
            UpdateShaderValue(InputProperty);
        }

    }

}
