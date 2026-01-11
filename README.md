# DotNet.AzureMaps.UI

## Summary
A companion UI library for [Marqdouj.DotNet.AzureMaps](https://www.nuget.org/packages/Marqdouj.DotNet.AzureMaps/).

## Demo
A demo of this, and other of my `DotNet` packages, can be found [here](https://github.com/marqdouj/dotnet.demo).

## Release Notes
### 10.7.0 - 10.6.2
- Update NuGet packages.

### 10.6.1
- `DialogExtensions`.
  - `GetDefaultDialogParameters`. 
	- Added new optional parameter `string? width = "80%"`. 
	- Was hard-coded to 60% previously (NOTE: you could always change it after you get the parameters).
- `UIModelInputValue`. Removed the `TextFieldType.Url` setting on all model `Url` input values.
  The `FluentTextField` has issues with relative Urls, so changing them to `Text` is a workaround for this issue.

### 10.6.0 - 10.5.0
- Updated [Marqdouj.DotNet.AzureMaps](https://www.nuget.org/packages/Marqdouj.DotNet.AzureMaps/) NuGet package.

### 10.4.0
- `UIModels (new)`.
  - `TrafficOptionsUIModel`. UI Model for map Traffic options.
- `UIModelInput`.
  - `Style`. Removed default; was causing issues with Select and AreaRestricted.
- `UIModelInputDialog`. Added new parameters:
  - `Width`. Default is '100%'.
  - `Style`. Default is ''.
- Updated [Marqdouj.DotNet.AzureMaps](https://www.nuget.org/packages/Marqdouj.DotNet.AzureMaps/) NuGet package.

### 10.3.9
- `UIModelInput`. Added new parameters:
  - `Width`. Default is '100%'.
  - `Style`. Default is 'overflow:auto;'.

### 10.3.8 - 10.3.1
- Updated [Marqdouj.DotNet.AzureMaps](https://www.nuget.org/packages/Marqdouj.DotNet.AzureMaps/) NuGet package.

### 10.3.0 (Major Changes - Non-Breaking)
- Major changes were done to [Marqdouj.DotNet.AzureMaps](https://www.nuget.org/packages/Marqdouj.DotNet.AzureMaps/).
- This package was updated to reflect those changes.

### 10.2.0 (Breaking Changes)
- Major refactor was done to [Marqdouj.DotNet.AzureMaps](https://www.nuget.org/packages/Marqdouj.DotNet.AzureMaps/).
- This package was updated to reflect those changes.

### 10.1.0 (Breaking Changes)
- `Naming Changes (Breaking Changes)`.
  - `UILayerDefInput` has been renamed to `UIModelInput`.
	- The name change aligns more with the actual functionality.
- `UIModels (new)`.
  - `CameraOptionsUIModel`. UI Model for map Camera options.
  - `StyleOptionsUIModel`. UI Model for map Style options.
- `DialogExtentions (New)`. Simplifies showing UI dialogs for layer and map options.
  - `ShowInputsDialog(this IDialogService service, ...)`

### 10.0.0
- Initial release
