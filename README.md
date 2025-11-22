# DotNet.AzureMaps.UI

## Summary
A companion UI library for [Marqdouj.NotNet.AzureMaps](https://www.nuget.org/packages/Marqdouj.DotNet.AzureMaps/).

## Demo
A demo of this, and other of my `DotNet` packages, can be found [here](https://github.com/marqdouj/dotnet.demo).

## Release Notes
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
