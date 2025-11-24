using Marqdouj.DotNet.AzureMaps.Map.Interop.Layers;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public class HeatMapLayerUIModel : MapLayerDefUIModel<HeatMapLayerDef>, ICloneable
    {
        private readonly HeatMapLayerOptionsUIModel options;

        public HeatMapLayerUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            options = new(xmlService);
            Source = new();
        }

        public override HeatMapLayerDef? Source
        {
            get => base.Source;
            set
            {
                value?.Options ??= new();
                value?.SourceOptions ??= new();

                base.Source = value;
                options.Source = value?.Options;
            }
        }

        public override List<IUIModelValue> ToUIList()
        {
            var items = base.ToUIList();
            items.RemoveAll(e => e.Name == nameof(HeatMapLayerDef.Options));
            items.AddRange(options.ToUIList());

            return [.. items.OrderBy(e => e.SortOrder).ThenBy(e => e.NameDisplay)];
        }

        public override List<IUIModelInputValue> ToUIInputList()
        {
            var items = base.ToUIInputList();
            items.AddRange(options.ToUIInputList());

            return [.. items.OrderBy(e => e.SortOrder).ThenBy(e => e.Model.NameDisplay)];
        }

        public object Clone()
        {
            var clone = (HeatMapLayerUIModel)this.MemberwiseClone();
            clone.Source = Source?.Clone() as HeatMapLayerDef;

            return clone;
        }

        public HeatMapLayerOptionsUIModel Options => options;
    }

    public class HeatMapLayerOptionsUIModel(IAzureMapsXmlService? xmlService) : LayerSourceOptionsUIModel<HeatMapLayerOptions>(xmlService)
    {
        public override List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(Color, UIModelInputType.Color),
                new UIModelInputValue(Intensity, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Opacity, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Radius, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Weight, UIModelInputType.Text, TextFieldType.Number),
            };

            items.AddRange(base.ToUIInputList());
            return items;
        }


        public IUIModelValue Color => GetItem(nameof(HeatMapLayerOptions.Color))!;
        public IUIModelValue Intensity => GetItem(nameof(HeatMapLayerOptions.Intensity))!;
        public IUIModelValue Opacity => GetItem(nameof(HeatMapLayerOptions.Opacity))!;
        public IUIModelValue Radius => GetItem(nameof(HeatMapLayerOptions.Radius))!;
        public IUIModelValue Weight => GetItem(nameof(HeatMapLayerOptions.Weight))!;
    }
}
