using Marqdouj.DotNet.AzureMaps.Map.Layers;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public class PolygonLayerUIModel : LayerUIModel<PolygonLayerDef>, ICloneable
    {
        private readonly PolygonLayerOptionsUIModel options;

        public PolygonLayerUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            options = new(xmlService);
            Source = new();
        }

        public override PolygonLayerDef? Source
        {
            get => base.Source;
            set
            {
                value?.Options ??= new();
                value?.DataSource ??= new();

                base.Source = value;
                options.Source = value?.Options;
            }
        }

        public override List<IUIModelValue> ToUIList()
        {
            var items = base.ToUIList();
            items.RemoveAll(e => e.Name == nameof(PolygonLayerDef.Options));
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
            var clone = (PolygonLayerUIModel)this.MemberwiseClone();
            clone.Source = Source?.Clone() as PolygonLayerDef;

            return clone;
        }

        public PolygonLayerOptionsUIModel Options => options;
    }

    public class PolygonLayerOptionsUIModel(IAzureMapsXmlService? xmlService) : LayerSourceOptionsUIModel<PolygonLayerOptions>(xmlService)
    {
        public override List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(FillAntialias, UIModelInputType.Select, lookup: UILookups.GetBooleans(true)),
                new UIModelInputValue(FillColor, UIModelInputType.Color),
                new UIModelInputValue(FillOpacity, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(FillPattern, UIModelInputType.Text, TextFieldType.Text),
            };

            items.AddRange(base.ToUIInputList());
            return items;
        }

        public IUIModelValue FillAntialias => GetItem(nameof(PolygonLayerOptions.FillAntialias))!;
        public IUIModelValue FillColor => GetItem(nameof(PolygonLayerOptions.FillColor))!;
        public IUIModelValue FillOpacity => GetItem(nameof(PolygonLayerOptions.FillOpacity))!;
        public IUIModelValue FillPattern => GetItem(nameof(PolygonLayerOptions.FillPattern))!;
    }
}
