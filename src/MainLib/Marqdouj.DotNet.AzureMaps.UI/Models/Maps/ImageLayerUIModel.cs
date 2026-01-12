using Marqdouj.DotNet.AzureMaps.Map.Layers;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.UI;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public class ImageLayerUIModel : LayerUIModel<ImageLayerDef>, ICloneable
    {
        private readonly ImageLayerOptionsUIModel options;

        public ImageLayerUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            options = new(xmlService);
            Source = new();

            options.Url.SortOrder = -1;
        }

        public override ImageLayerDef? Source
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
            items.RemoveAll(e => e.Name == nameof(ImageLayerDef.Options));
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
            var clone = (ImageLayerUIModel)this.MemberwiseClone();
            clone.Source = Source?.Clone() as ImageLayerDef;

            return clone;
        }

        public ImageLayerOptionsUIModel Options => options;
    }

    public class ImageLayerOptionsUIModel : LayerMediaOptionsUIModel<ImageLayerOptions>
    {
        public ImageLayerOptionsUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            Url?.NameAlias = "Image URL";
            Url?.ReadOnly = true;
        }

        public override List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                //new UIModelInputValue(Coordinates, UIModelInputType.ImageCoordinates),
                new UIModelInputValue(Url, UIModelInputType.Text){ Style="width:350px;" },
            };

            items.AddRange(base.ToUIInputList());
            return items;
        }

        public IUIModelValue Coordinates => GetItem(nameof(ImageLayerOptions.Coordinates))!;
        public IUIModelValue Url => GetItem(nameof(ImageLayerOptions.Url))!;
    }
}
