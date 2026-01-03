using Marqdouj.DotNet.AzureMaps.Map.Layers;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public class TileLayerUIModel : LayerUIModel<TileLayerDef>, ICloneable
    {
        private readonly TileLayerOptionsUIModel options;

        public TileLayerUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            options = new(xmlService);
            Source = new();

            options.TileUrl.SortOrder = -1;
        }

        public override TileLayerDef? Source
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
            items.RemoveAll(e => e.Name == nameof(TileLayerDef.Options));
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
            var clone = (TileLayerUIModel)this.MemberwiseClone();
            clone.Source = Source?.Clone() as TileLayerDef;

            return clone;
        }

        public TileLayerOptionsUIModel Options => options;
    }

    public class TileLayerOptionsUIModel(IAzureMapsXmlService? xmlService) : LayerMediaOptionsUIModel<TileLayerOptions>(xmlService)
    {
        private static readonly List<Option<string>> booleans = UILookups.GetBooleans(true);

        public override List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                //new UIModelInputValue(Bounds, UIModelInputType.BoundingBox),
                new UIModelInputValue(IsTMS, UIModelInputType.Select, lookup: booleans),
                new UIModelInputValue(MinSourceZoom, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(MaxSourceZoom, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(TileSize, UIModelInputType.Text, TextFieldType.Number),
                //new UIModelInputValue(Subdomains, UIModelInputType.Text),
                new UIModelInputValue(TileUrl, UIModelInputType.Text){ Style="width:350px;" },
            };

            items.AddRange(base.ToUIInputList());
            return items;
        }

        public IUIModelValue Bounds => GetItem(nameof(TileLayerOptions.Bounds))!;
        public IUIModelValue IsTMS => GetItem(nameof(TileLayerOptions.IsTMS))!;
        public IUIModelValue MinSourceZoom => GetItem(nameof(TileLayerOptions.MinSourceZoom))!;
        public IUIModelValue MaxSourceZoom => GetItem(nameof(TileLayerOptions.MaxSourceZoom))!;
        public IUIModelValue TileSize => GetItem(nameof(TileLayerOptions.TileSize))!;
        //public IUIModelValue Subdomains => GetItem(nameof(TileLayerOptions.Subdomains))!;
        public IUIModelValue TileUrl => GetItem(nameof(TileLayerOptions.TileUrl))!;
    }
}
