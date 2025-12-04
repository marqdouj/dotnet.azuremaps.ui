using Marqdouj.DotNet.AzureMaps.Map.Layers;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.UI;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public interface ILayerUIModel : IUIModel, IUIInputListSource
    {
        IUIModelValue Before { get; }
        IUIModelValue Id { get; }
        IUIModelValue LayerType { get; }
        IUIModelValue SourceId { get; }
        IUIModelValue SourceUrl { get; }

        DataSourceUIModel DataSource { get; }
        MapLayerDef LayerDef { get; }
    }

    public abstract class LayerUIModel<T> : XmlUIModel<T>, ILayerUIModel where T : MapLayerDef
    {
        private readonly DataSourceUIModel dataSource;

        protected internal LayerUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            dataSource = new(xmlService);

            LayerType.SortOrder = -4;
            Id.SortOrder = -3;
            dataSource.Id.SortOrder = -2;
            dataSource.Url.SortOrder = -1;
        }

        public MapLayerDef LayerDef => Source!;

        public override T? Source 
        { 
            get => base.Source; 
            set
            {
                base.Source = value;
                dataSource.Source = value?.DataSource ?? new();
            }
        }

        public override List<IUIModelValue> ToUIList()
        {
            var items = base.ToUIList();
            items.RemoveAll(e => e.Name == nameof(MapLayerDef.DataSource));
            items.AddRange(dataSource.ToUIList());

            return [.. items.OrderBy(e => e.SortOrder).ThenBy(e => e.NameDisplay)];
        }

        public virtual List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(Before, UIModelInputType.Text),
                new UIModelInputValue(Id, UIModelInputType.Text),
            };
            items.AddRange(dataSource.ToUIInputList());

            return items;
        }

        public DataSourceUIModel DataSource => dataSource;

        public IUIModelValue Before => GetItem(nameof(MapLayerDef.Before))!;
        public IUIModelValue Id => GetItem(nameof(MapLayerDef.Id))!;
        public IUIModelValue LayerType => GetItem(nameof(MapLayerDef.LayerType))!;
        public IUIModelValue SourceId => dataSource.Id!;
        public IUIModelValue SourceUrl => dataSource.Url!;
    }
}
