// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;

namespace MDS.ShaderEffects {

    /// <summary>
    /// A shader effect that adjusts the luma value of a target texture in the HCY' color space
    /// </summary>
    public class AdjustLumaEffect : ShaderEffect {

        #region Dependency Properties

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(AdjustLumaEffect), 0);

        public static readonly DependencyProperty DeltaProperty =
            DependencyProperty.Register("Delta", typeof(double), typeof(AdjustLumaEffect),
                    new UIPropertyMetadata(0.5, PixelShaderConstantCallback(0)), OnValidateDelta);

        public static readonly DependencyProperty CoefficientsProperty =
            DependencyProperty.Register("Coefficients", typeof(Point3D), typeof(AdjustLumaEffect),
                    new UIPropertyMetadata(Rec609, PixelShaderConstantCallback(1)), OnValidateChromaticities);

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
        /// Luma change between -1.0 and 1.0
        /// </summary>
        public double Delta {
            get {
                return (double)GetValue(DeltaProperty);
            }
            set {
                SetValue(DeltaProperty, value);
            }
        }

        /// <summary>
        /// Coefficients that are considered when weighting the RGB channels
        /// of the input (one components for each channel between 0 and 1.0)
        /// </summary>
        public Point3D Coefficients {
            get {
                return (Point3D)GetValue(CoefficientsProperty);
            }
            set {
                SetValue(CoefficientsProperty, value);
            }
        }

        /// <summary>
        /// Coefficients for the ITU-R Recommendation BT.601 standard
        /// </summary>
        public static Point3D Rec609 {
            get {
                return new Point3D(0.2126, 0.7152, 0.0722);
            }
        }

        /// <summary>
        /// Coefficients for the ITU-R Recommendation BT.709 standard
        /// </summary>
        public static Point3D Rec709 {
            get {
                return new Point3D(0.2126, 0.7152, 0.0722);
            }
        }

        /// <summary>
        /// Coefficients for the SMPTE 240M specification
        /// </summary>
        public static Point3D SMPTE240M {
            get {
                return new Point3D(0.212, 0.701, 0.087);
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

        private static bool OnValidateChromaticities(object value) {
            if (value is Point3D) {
                Point3D p = (Point3D)value;
                return p.X >= 0.0 && p.X <= 1.0 && p.Y >= 0.0 && p.Y <= 1.0 && p.Z >= 0.0 && p.Z <= 1.0;
            }
            return false;
        }

        public AdjustLumaEffect() {
            PixelShader = new PixelShader {
                UriSource = Global.MakePackUri("Shaders/Adjust/Luma.ps")
            };
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(DeltaProperty);
            UpdateShaderValue(CoefficientsProperty);
        }

    }

}
