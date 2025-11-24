using Marqdouj.DotNet.AzureMaps.Map.Options;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps.Options
{
    public class CameraOptionsUIModel : XmlUIModel<CameraOptions>, IUIInputListSource
    {
        public CameraOptionsUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            Bearing?.SetBindMinMax(-180, 180);
            Pitch?.SetBindMinMax(0, 60);
            Zoom?.SetBindMinMax(0, 24);
        }

        public virtual List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(Bearing, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Center, UIModelInputType.Position),
                new UIModelInputValue(CenterOffset, UIModelInputType.Pixel),
                new UIModelInputValue(MaxPitch, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(MaxZoom, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(MinPitch, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(MinZoom, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Pitch, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Zoom, UIModelInputType.Text, TextFieldType.Number),
            };

            return items;
        }

        public IUIModelValue Bearing => GetItem(nameof(CameraOptions.Bearing))!;
        public IUIModelValue Center => GetItem(nameof(CameraOptions.Center))!;
        public IUIModelValue CenterOffset => GetItem(nameof(CameraOptions.CenterOffset))!;
        public IUIModelValue MaxPitch => GetItem(nameof(CameraOptions.MaxPitch))!;
        public IUIModelValue MaxZoom => GetItem(nameof(CameraOptions.MaxZoom))!;
        public IUIModelValue MinPitch => GetItem(nameof(CameraOptions.MinPitch))!;
        public IUIModelValue MinZoom => GetItem(nameof(CameraOptions.MinZoom))!;
        public IUIModelValue Pitch => GetItem(nameof(CameraOptions.Pitch))!;
        public IUIModelValue Zoom => GetItem(nameof(CameraOptions.Zoom))!;
    }
}
