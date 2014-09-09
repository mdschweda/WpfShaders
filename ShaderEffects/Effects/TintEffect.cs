// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MDS.ShaderEffects {

    /// <summary>
    /// A shader effect that colorizes target texture
    /// </summary>
    public class TintEffect : ShaderEffect {

        #region Dependency Properties

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(TintEffect), 0);

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(TintEffect),
                    new UIPropertyMetadata(Colors.Cyan, PixelShaderConstantCallback(0)));

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
        /// Color of the result
        /// </summary>
        public Color Color {
            get {
                return (Color)GetValue(ColorProperty);
            }
            set {
                SetValue(ColorProperty, value);
            }
        }

        #endregion

        public TintEffect() {
            PixelShader = new PixelShader {
                UriSource = Global.MakePackUri("Shaders/Tint.ps")
            };
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(ColorProperty);
        }

    }

}
