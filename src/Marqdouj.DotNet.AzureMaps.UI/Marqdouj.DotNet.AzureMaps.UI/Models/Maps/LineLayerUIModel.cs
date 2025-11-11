using Marqdouj.DotNet.AzureMaps.Map.Interop.Layers;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.FluentUI.UIInput;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public class LineLayerUIModel : MapLayerDefUIModel<LineLayerDef>, ICloneable
    {
        private readonly LineLayerOptionsUIModel options;

        public LineLayerUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            options = new(xmlService);
            Source = new();
        }

        public override LineLayerDef? Source
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
            items.RemoveAll(e => e.Name == nameof(LineLayerDef.Options));
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
            var clone = (LineLayerUIModel)this.MemberwiseClone();
            clone.Source = Source?.Clone() as LineLayerDef;

            return clone;
        }

        public LineLayerOptionsUIModel Options => options;
    }

    public class LineLayerOptionsUIModel(IAzureMapsXmlService? xmlService) : LayerSourceOptionsUIModel<LineLayerOptions>(xmlService)
    {
        private static readonly List<Option<string>> lineCaps = UIExtensions.GetEnumLookup<LineCap>(true);
        private static readonly List<Option<string>> lineJoins = UIExtensions.GetEnumLookup<LineJoin>(true);
        private static readonly List<Option<string>> anchors = UIExtensions.GetEnumLookup<TranslateAnchor>(true);

        public override List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(Blur, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(LineCap, UIModelInputType.Select, lookup: lineCaps),
                new UIModelInputValue(LineJoin, UIModelInputType.Select, lookup: lineJoins),
                new UIModelInputValue(Offset, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(StrokeColor, UIModelInputType.Color),
                new UIModelInputValue(StrokeOpacity, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(StrokeWidth, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Translate, UIModelInputType.Pixel),
                new UIModelInputValue(TranslateAnchor, UIModelInputType.Select, lookup: anchors),
            };

            items.AddRange(base.ToUIInputList());
            return items;
        }

        public IUIModelValue Blur => GetItem(nameof(LineLayerOptions.Blur))!;
        public IUIModelValue LineCap => GetItem(nameof(LineLayerOptions.LineCap))!;
        public IUIModelValue LineJoin => GetItem(nameof(LineLayerOptions.LineJoin))!;
        public IUIModelValue Offset => GetItem(nameof(LineLayerOptions.Offset))!;
        public IUIModelValue StrokeColor => GetItem(nameof(LineLayerOptions.StrokeColor))!;
        public IUIModelValue StrokeOpacity => GetItem(nameof(LineLayerOptions.StrokeOpacity))!;
        public IUIModelValue StrokeWidth => GetItem(nameof(LineLayerOptions.StrokeWidth))!;
        public IUIModelValue Translate => GetItem(nameof(LineLayerOptions.Translate))!;
        public IUIModelValue TranslateAnchor => GetItem(nameof(LineLayerOptions.TranslateAnchor))!;
    }
}
