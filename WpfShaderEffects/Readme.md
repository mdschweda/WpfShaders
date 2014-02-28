Library with shader effects for the use with WPF.

# Build

The projects makes use of the Shader Effects BuildTask from [WPF Futures](https://wpf.codeplex.com/releases/view/14962).
Since some of the effects require pixel shader 3.0 ShaderBuildTask.dll needs to be replaced with a modified version.

To do so install Shader Effects BuildTask and copy the provided version under \ShaderEffectLibrary\ to
%WinDir%\assembly\GAC_32\ShaderBuildTask\%Version% as administrator. This allows you to compile .fx files under the
[ps_3_0](http://msdn.microsoft.com/en-us/library/windows/desktop/bb219845%28v=vs.85%29.aspx) profile.

# Effects

## BlendEffect

Blend effects identical to Adobe PhotoShop.

### Parameters

* **Base *(Effect target)***: Background brush
* **Blend**: Foreground brush
* **Amount**: Intensity of the blend effect (0-1.0)
* **Mode**: Blend mode

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
