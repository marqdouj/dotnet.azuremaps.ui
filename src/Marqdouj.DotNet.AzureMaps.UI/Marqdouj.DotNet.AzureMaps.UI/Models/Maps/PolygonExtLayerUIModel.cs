using Marqdouj.DotNet.AzureMaps.Map.Interop.Layers;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.FluentUI.UIInput;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public class PolygonExtLayerUIModel : MapLayerDefUIModel<PolygonExtLayerDef>, ICloneable
    {
        private readonly PolygonExtLayerOptionsUIModel options;

        public PolygonExtLayerUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            options = new(xmlService);
            Source = new();
        }

        public override PolygonExtLayerDef? Source
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
            items.RemoveAll(e => e.Name == nameof(PolygonExtLayerDef.Options));
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
            var clone = (PolygonExtLayerUIModel)this.MemberwiseClone();
            clone.Source = Source?.Clone() as PolygonExtLayerDef;

            return clone;
        }

        public PolygonExtLayerOptionsUIModel Options => options;
    }

    public class PolygonExtLayerOptionsUIModel(IAzureMapsXmlService? xmlService) : LayerSourceOptionsUIModel<PolygonExtLayerOptions>(xmlService)
    {
        private static readonly List<Option<string>> anchors = UIExtensions.GetEnumLookup<TranslateAnchor>(true);

        public override List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(Base, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(FillColor, UIModelInputType.Color),
                new UIModelInputValue(FillOpacity, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(FillPattern, UIModelInputType.Text, TextFieldType.Text),
                new UIModelInputValue(Height, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Translate, UIModelInputType.Pixel),
                new UIModelInputValue(TranslateAnchor, UIModelInputType.Select, lookup: anchors),
                new UIModelInputValue(VerticalGradient, UIModelInputType.Select, lookup: anchors),
            };

            items.AddRange(base.ToUIInputList());
            return items;
        }

        public IUIModelValue Base => GetItem(nameof(PolygonExtLayerOptions.Base))!;
        public IUIModelValue FillColor => GetItem(nameof(PolygonExtLayerOptions.FillColor))!;
        public IUIModelValue FillOpacity => GetItem(nameof(PolygonExtLayerOptions.FillOpacity))!;
        public IUIModelValue FillPattern => GetItem(nameof(PolygonExtLayerOptions.FillPattern))!;
        public IUIModelValue Height => GetItem(nameof(PolygonExtLayerOptions.Height))!;
        public IUIModelValue Translate => GetItem(nameof(PolygonExtLayerOptions.Translate))!;
        public IUIModelValue TranslateAnchor => GetItem(nameof(PolygonExtLayerOptions.TranslateAnchor))!;
        public IUIModelValue VerticalGradient => GetItem(nameof(PolygonExtLayerOptions.VerticalGradient))!;
    }
}
