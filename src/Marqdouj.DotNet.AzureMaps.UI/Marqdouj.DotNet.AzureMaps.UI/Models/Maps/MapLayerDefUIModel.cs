using Marqdouj.DotNet.AzureMaps.Map.Interop.Layers;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.UI;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public interface ILayerDefUIModel : IUIModel
    {
        IUIModelValue Before { get; }
        IUIModelValue Id { get; }
        IUIModelValue MapType { get; }
        IUIModelValue SourceId { get; }
        IUIModelValue SourceUrl { get; }

        MapLayerDef? LayerDef { get;  }
        List<IUIModelInputValue> ToUIInputList();
        SourceOptionsUIModel SourceOptions { get; }
    }

    public abstract class MapLayerDefUIModel<T> : XmlUIModel<T>, ILayerDefUIModel where T : MapLayerDef
    {
        private readonly SourceOptionsUIModel sourceUI;

        protected internal MapLayerDefUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            sourceUI = new(xmlService);

            //Place the important information at the top.
            MapType.SortOrder = -4;
            Id.SortOrder = -3;
            SourceId.SortOrder = -2;
            SourceUrl.SortOrder = -1;
        }

        public MapLayerDef? LayerDef => Source;

        public override T? Source 
        { 
            get => base.Source; 
            set
            {
                base.Source = value;
                sourceUI.Source = value?.SourceOptions;
            }
        }

        public override List<IUIModelValue> ToUIList()
        {
            var items = base.ToUIList();
            items.RemoveAll(e => e.Name == nameof(MapLayerDef.SourceOptions));

            return [.. items.OrderBy(e => e.SortOrder).ThenBy(e => e.NameDisplay)];
        }

        public virtual List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(Before, UIModelInputType.Text),
                new UIModelInputValue(Id, UIModelInputType.Text),
                new UIModelInputValue(SourceId, UIModelInputType.Text),
                new UIModelInputValue(SourceUrl, UIModelInputType.Text){ Style="width:350px;" },
            };

            return items;
        }

        public SourceOptionsUIModel SourceOptions => sourceUI;

        public IUIModelValue Before => GetItem(nameof(MapLayerDef.Before))!;
        public IUIModelValue Id => GetItem(nameof(MapLayerDef.Id))!;
        public IUIModelValue SourceId => GetItem(nameof(MapLayerDef.SourceId))!;
        public IUIModelValue SourceUrl => GetItem(nameof(MapLayerDef.SourceUrl))!;
        public IUIModelValue MapType => GetItem(nameof(MapLayerDef.Type))!;
    }
}
