using Marqdouj.DotNet.AzureMaps.Map.Layers;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public abstract class LayerOptionsUIModel<T> : XmlUIModel<T> where T : LayerOptions
    {
        protected internal LayerOptionsUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            MaxZoom.SetBindMinMax(0, 24);
            MinZoom.SetBindMinMax(0, 24);
        }

        public virtual List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(MaxZoom, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(MinZoom, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Visible, UIModelInputType.Select, lookup: UILookups.GetBooleans(true)),
            };

            return items;
        }

        public IUIModelValue MaxZoom => GetItem(nameof(LayerOptions.MaxZoom))!;
        public IUIModelValue MinZoom => GetItem(nameof(LayerOptions.MinZoom))!;
        public IUIModelValue Visible => GetItem(nameof(LayerOptions.Visible))!;
    }

    public abstract class LayerMediaOptionsUIModel<T> : LayerOptionsUIModel<T> where T : MediaLayerOptions
    {
        protected internal LayerMediaOptionsUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            Contrast.SetBindMinMax(-1, 1);
            FadeDuration.SetBindMinMax(0, int.MaxValue);
            HueRotation.SetBindMinMax(-360, 360);
            MaxBrightness.SetBindMinMax(0, 1);
            MinBrightness.SetBindMinMax(0, 1);
            Opacity.SetBindMinMax(0, 1);
            Saturation.SetBindMinMax(-1, 1);
        }

        public override List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(Contrast, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(FadeDuration, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(HueRotation, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(MaxBrightness, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(MinBrightness, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Opacity, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Saturation, UIModelInputType.Text, TextFieldType.Number),
            };

            items.AddRange(base.ToUIInputList());

            return items;
        }

        public IUIModelValue Contrast => GetItem(nameof(MediaLayerOptions.Contrast))!;
        public IUIModelValue FadeDuration => GetItem(nameof(MediaLayerOptions.FadeDuration))!;
        public IUIModelValue HueRotation => GetItem(nameof(MediaLayerOptions.HueRotation))!;
        public IUIModelValue MaxBrightness => GetItem(nameof(MediaLayerOptions.MaxBrightness))!;
        public IUIModelValue MinBrightness => GetItem(nameof(MediaLayerOptions.MinBrightness))!;
        public IUIModelValue Opacity => GetItem(nameof(MediaLayerOptions.Opacity))!;
        public IUIModelValue Saturation => GetItem(nameof(MediaLayerOptions.Saturation))!;
    }

    public abstract class LayerSourceOptionsUIModel<T> : LayerOptionsUIModel<T> where T : SourceLayerOptions
    {
        protected internal LayerSourceOptionsUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            SourceId.NameAlias = "SourceId";
            SourceLayerId.NameAlias = "SourceLayerId";
        }

        public override List<IUIModelValue> ToUIList()
        {
            var items = base.ToUIList();
            items.RemoveAll(e => e.Name == nameof(SourceLayerOptions.Source));
            return items;
        }

        public override List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(SourceId, UIModelInputType.Text),
                new UIModelInputValue(SourceLayerId, UIModelInputType.Text),
            };

            items.AddRange(base.ToUIInputList());

            return items;
        }

        public IUIModelValue SourceId => GetItem(nameof(SourceLayerOptions.Source))!;
        public IUIModelValue SourceLayerId => GetItem(nameof(SourceLayerOptions.SourceLayer))!;
    }
}
