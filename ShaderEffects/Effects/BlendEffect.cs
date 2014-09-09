// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MDS.ShaderEffects {

    /// <summary>
    /// Blend modes for mixing colors
    /// </summary>
    public enum BlendModes {
        Normal,
        Darken,
        Multiply,
        ColorBurn,
        LinearBurn,
        DarkerColor,
        Lighten,
        Screen,
        ColorDodge,
        LinearDodge,
        LighterColor,
        Overlay,
        SoftLight,
        HardLight,
        VividLight,
        LinearLight,
        PinLight,
        HardMix,
        Difference,
        Exclusion,
        Subtract,
        Divide,
        Hue,
        Saturation,
        Color,
        Luminosity
    }

    /// <summary>
    /// A shader effect that mixes two textures
    /// </summary>
    public class BlendEffect : ShaderEffect {

        #region Dependency Properties

        public static readonly DependencyProperty BaseProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Base", typeof(BlendEffect), 0);

        public static readonly DependencyProperty BlendProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Blend", typeof(BlendEffect), 1);

        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(BlendModes), typeof(BlendEffect),
                new PropertyMetadata(BlendModes.Normal, OnModeChanged));

        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register("Amount", typeof(double), typeof(BlendEffect),
                    new UIPropertyMetadata(0.5, PixelShaderConstantCallback(0)), OnValidateAmount);

        /// <summary>
        /// Brush that acts as the input for the background layer
        /// </summary>
        public Brush Base {
            get {
                return (Brush)GetValue(BaseProperty);
            }
            set {
                SetValue(BaseProperty, value);
            }
        }

        /// <summary>
        /// Brush that acts as the input for the foreground layer
        /// </summary>
        public Brush Blend {
            get {
                return (Brush)GetValue(BlendProperty);
            }
            set {
                SetValue(BlendProperty, value);
            }
        }

        /// <summary>
        /// Intensity of the color blending between 0 and 1.0
        /// </summary>
        public double Amount {
            get {
                return (double)GetValue(AmountProperty);
            }
            set {
                SetValue(AmountProperty, value);
            }
        }
        
        /// <summary>
        /// Blend mode for calculating the result layer
        /// </summary>
        public BlendModes Mode {
            get {
                return (BlendModes)GetValue(ModeProperty);
            }
            set {
                SetValue(ModeProperty, value);
            }
        }

        #endregion

        private static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var effect = d as BlendEffect;
            if (effect != null) {
                string shader = "Shaders/Blend/Normal.ps";
                if (e.NewValue is BlendModes) {
                    switch ((BlendModes)e.NewValue) {
                        case BlendModes.Darken:
                            shader = "Shaders/Blend/Darken.ps";
                            break;
                        case BlendModes.Multiply:
                            shader = "Shaders/Blend/Multiply.ps";
                            break;
                        case BlendModes.ColorBurn:
                            shader = "Shaders/Blend/ColorBurn.ps";
                            break;
                        case BlendModes.LinearBurn:
                            shader = "Shaders/Blend/LinearBurn.ps";
                            break;
                        case BlendModes.DarkerColor:
                            shader = "Shaders/Blend/DarkerColor.ps";
                            break;
                        case BlendModes.Lighten:
                            shader = "Shaders/Blend/Lighten.ps";
                            break;
                        case BlendModes.Screen:
                            shader = "Shaders/Blend/Screen.ps";
                            break;
                        case BlendModes.ColorDodge:
                            shader = "Shaders/Blend/ColorDodge.ps";
                            break;
                        case BlendModes.LinearDodge:
                            shader = "Shaders/Blend/LinearDodge.ps";
                            break;
                        case BlendModes.LighterColor:
                            shader = "Shaders/Blend/LighterColor.ps";
                            break;
                        case BlendModes.Overlay:
                            shader = "Shaders/Blend/Overlay.ps";
                            break;
                        case BlendModes.SoftLight:
                            shader = "Shaders/Blend/SoftLight.ps";
                            break;
                        case BlendModes.HardLight:
                            shader = "Shaders/Blend/HardLight.ps";
                            break;
                        case BlendModes.VividLight:
                            shader = "Shaders/Blend/VividLight.ps";
                            break;
                        case BlendModes.LinearLight:
                            shader = "Shaders/Blend/LinearLight.ps";
                            break;
                        case BlendModes.PinLight:
                            shader = "Shaders/Blend/PinLight.ps";
                            break;
                        case BlendModes.HardMix:
                            shader = "Shaders/Blend/HardMix.ps";
                            break;
                        case BlendModes.Difference:
                            shader = "Shaders/Blend/Difference.ps";
                            break;
                        case BlendModes.Exclusion:
                            shader = "Shaders/Blend/Exclusion.ps";
                            break;
                        case BlendModes.Subtract:
                            shader = "Shaders/Blend/Subtract.ps";
                            break;
                        case BlendModes.Divide:
                            shader = "Shaders/Blend/Divide.ps";
                            break;
                        case BlendModes.Hue:
                            shader = "Shaders/Blend/Hue.ps";
                            break;
                        case BlendModes.Saturation:
                            shader = "Shaders/Blend/Saturation.ps";
                            break;
                        case BlendModes.Color:
                            shader = "Shaders/Blend/Color.ps";
                            break;
                        case BlendModes.Luminosity:
                            shader = "Shaders/Blend/Luminosity.ps";
                            break;
                    }
                }
                effect.PixelShader.UriSource = Global.MakePackUri(shader);
            }
        }

        private static bool OnValidateAmount(object value) {
            if (value is double) {
                double amount = (double)value;
                return amount >= 0 && amount <= 1.0;
            }
            return false;
        }

        public BlendEffect() {
            PixelShader = new PixelShader {
                UriSource = Global.MakePackUri("Shaders/Blend/Normal.ps")
            };
            Mode = BlendModes.Normal;
            UpdateShaderValue(BaseProperty);
            UpdateShaderValue(BlendProperty);
            UpdateShaderValue(AmountProperty);
        }

    }

}
