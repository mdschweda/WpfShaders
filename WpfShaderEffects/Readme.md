Library with shader effects for use with WPF.

# Build

The projects makes use of the Shader Effects BuildTask from [WPF Futures](https://wpf.codeplex.com/releases/view/14962).
Since some of the effects require pixel shader 3.0 ShaderBuildTask.dll needs to be replaced with a modified version.

To do so install Shader Effects BuildTask and copy the provided version under \ShaderEffectLibrary\ to
%WinDir%\assembly\GAC_32\ShaderBuildTask\%Version% as administrator. This allows you to compile .fx files under the
[ps_3_0](http://msdn.microsoft.com/en-us/library/windows/desktop/bb219845%28v=vs.85%29.aspx) profile.

# Effects

## AdjustChromaEffect

Adjusts the chroma value of a target texture in the HCY' color space.

### Parameters

* **Input**: Brush that acts as the input
* **Delta**: Chroma change between -1.0 and 1.0

## AdjustHueEffect

Adjusts the hue of a target texture.

### Parameters

* **Input**: Brush that acts as the input
* **Delta**: Hue change between -1.0 and 1.0

## AdjustLightnessEffect

Adjusts the lightness of a target texture.

### Parameters

* **Input**: Brush that acts as the input
* **Delta**: Lightness change between -1.0 and 1.0

## AdjustLumaEffect

Adjusts the luma value of a target texture in the HCY' color space.

### Parameters

* **Input**: Brush that acts as the input
* **Delta**: Luma change between -1.0 and 1.0
* **Coefficients**: Coefficients that are considered when weighting the RGB channels of the input (one components
  for each channel between 0 and 1.0)

### Predefined coefficients

* **Rec609**: ITU-R Recommendation BT.601 standard
* **Rec709**: ITU-R Recommendation BT.709 standard
* **SMPTE240M**: SMPTE 240M specification

## AdjustSaturationEffect

Adjusts the saturation of a target texture.

### Parameters

* **Input**: Brush that acts as the input
* **Delta**: Saturation change between -1.0 and 1.0

## BlendEffect

Blend effects identical to Adobe PhotoShop.

### Parameters

* **Base**: Brush that acts as the input for the background layer
* **Blend**: Brush that acts as the input for the foreground layer
* **Amount**: Intensity of the color blending between 0 and 1.0
* **Mode**: Blend mode for calculating the result layer

### Supported blend modes

* Normal
* Darken group
  * Darken
  * Multiply
  * Color Burn
  * Linear Burn
  * Darker Color
* Lighten group
  * Lighten
  * Screen
  * Color Dodge
  * Linear Dodge (Add)
  * Lighter Color
* Contrast group
  * Overlay
  * Soft Light
  * Hard Light
  * Vivid Light
  * Linear Light
  * Pin Light
  * Hard Mix
* Inversion group
  * Difference
  * Exclusion
* Cancellation group
  * Subtract
  * Divide
* Component group
  * Hue
  * Saturation
  * Color
  * Luminosity

## ChromaticAberrationEffect

Shifts the channels of a target texture, simulating achromatism.

### Parameters

* **Input**: Brush that acts as the input
* **OffsetR**: Offset by which the red channel is shifted
* **OffsetG**: Offset by which the green channel is shifted
* **OffsetB**: Offset by which the blue channel is shifted

## EdgeDetectionEffect

Accentuates edges in a target texture.

### Parameters

* **Input**: Brush that acts as the input

## GammaCorrectionEffect

Provides gamma correction.

### Parameters

* **Input**: Brush that acts as the input
* **Gamma**: A positive gamma value

## GrayscaleEffect

Turns a target texture into a grayscale output.

### Parameters

* **Input**: Brush that acts as the input

## InvertEffect

Inverts the colors of a target texture.

### Parameters

* **Input**: Brush that acts as the input

## SharpenEffect

Applies an unsharp mask on a target texture.

### Parameters

* **Input**: Brush that acts as the input

## SolarizeEffect

Solarization of a target texture.

### Parameters

* **Input**: Brush that acts as the input
* **Threshold**: Threshold for inversion of color channels between 0 and 1.0

## TintEffect

Colorizes a target texture.

### Parameters

* **Input**: Brush that acts as the input
* **Color**: Color of the result

