using Marqdouj.DotNet.AzureMaps.Map.Layers;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.FluentUI.UIInput;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public class BubbleLayerUIModel : LayerUIModel<BubbleLayerDef>, ICloneable
    {
        private readonly BubbleLayerOptionsUIModel options;

        public BubbleLayerUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            options = new(xmlService);
            Source = new();
        }

        public override BubbleLayerDef? Source 
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
            items.RemoveAll(e => e.Name == nameof(BubbleLayerDef.Options));
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
            var clone = (BubbleLayerUIModel)this.MemberwiseClone();
            clone.Source = Source?.Clone() as BubbleLayerDef;

            return clone;
        }

        public BubbleLayerOptionsUIModel Options => options;
    }

    public class BubbleLayerOptionsUIModel(IAzureMapsXmlService? xmlService) : LayerSourceOptionsUIModel<BubbleLayerOptions>(xmlService)
    {
        private static readonly List<Option<string>> pitches = UIExtensions.GetEnumLookup<BubbleLayerPitchAlignment>(true);

        public override List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(Color, UIModelInputType.Color),
                new UIModelInputValue(Blur, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Opacity, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(StrokeColor, UIModelInputType.Color),
                new UIModelInputValue(StrokeOpacity, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(StrokeWidth, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(PitchAlignment, UIModelInputType.Select, lookup: pitches),
                new UIModelInputValue(Radius, UIModelInputType.Text, TextFieldType.Number)
            };

            items.AddRange(base.ToUIInputList());
            return items;
        }

        public IUIModelValue Color => GetItem(nameof(BubbleLayerOptions.Color))!;
        public IUIModelValue Blur => GetItem(nameof(BubbleLayerOptions.Blur))!;
        public IUIModelValue Opacity => GetItem(nameof(BubbleLayerOptions.Opacity))!;
        public IUIModelValue StrokeColor => GetItem(nameof(BubbleLayerOptions.StrokeColor))!;
        public IUIModelValue StrokeOpacity => GetItem(nameof(BubbleLayerOptions.StrokeOpacity))!;
        public IUIModelValue StrokeWidth => GetItem(nameof(BubbleLayerOptions.StrokeWidth))!;
        public IUIModelValue PitchAlignment => GetItem(nameof(BubbleLayerOptions.PitchAlignment))!;
        public IUIModelValue Radius => GetItem(nameof(BubbleLayerOptions.Radius))!;
    }
}
