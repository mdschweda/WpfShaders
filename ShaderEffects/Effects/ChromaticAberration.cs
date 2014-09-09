// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MDS.ShaderEffects {

    /// <summary>
    /// A shader effect that shifts the channels of a target texture, simulating achromatism 
    /// </summary>
    public class ChromaticAberrationEffect : ShaderEffect {

        #region Dependency Properties

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(ChromaticAberrationEffect), 0);

        public static readonly DependencyProperty OffsetRProperty =
            DependencyProperty.Register("OffsetR", typeof(Point), typeof(ChromaticAberrationEffect),
                    new UIPropertyMetadata(new Point(0.0, 0.0), PixelShaderConstantCallback(0)));

        public static readonly DependencyProperty OffsetGProperty =
            DependencyProperty.Register("OffsetG", typeof(Point), typeof(ChromaticAberrationEffect),
                    new UIPropertyMetadata(new Point(1.0, 2.0), PixelShaderConstantCallback(1)));

        public static readonly DependencyProperty OffsetBProperty =
            DependencyProperty.Register("OffsetB", typeof(Point), typeof(ChromaticAberrationEffect),
                    new UIPropertyMetadata(new Point(4.0, 0.0), PixelShaderConstantCallback(2)));

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
        /// Offset by which the red channel is shifted
        /// </summary>
        public Point OffsetR {
            get {
                return (Point)GetValue(OffsetRProperty);
            }
            set {
                SetValue(OffsetRProperty, value);
            }
        }

        /// <summary>
        /// Offset by which the green channel is shifted
        /// </summary>
        public Point OffsetG {
            get {
                return (Point)GetValue(OffsetGProperty);
            }
            set {
                SetValue(OffsetGProperty, value);
            }
        }

        /// <summary>
        /// Offset by which the blue channel is shifted
        /// </summary>
        public Point OffsetB {
            get {
                return (Point)GetValue(OffsetBProperty);
            }
            set {
                SetValue(OffsetBProperty, value);
            }
        }

        #endregion

        public ChromaticAberrationEffect() {
            PixelShader = new PixelShader {
                UriSource = Global.MakePackUri("Shaders/ChromaticAberration.ps")
            };
            DdxUvDdyUvRegisterIndex = 3;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(OffsetRProperty);
            UpdateShaderValue(OffsetGProperty);
            UpdateShaderValue(OffsetBProperty);
        }

    }

}
