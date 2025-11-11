using Marqdouj.DotNet.AzureMaps.Map.Options;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public class SourceOptionsUIModel : XmlUIModel<DataSourceOptions>
    {
        private static readonly List<Option<string>> booleans = UILookups.GetBooleans(true);

        internal SourceOptionsUIModel(IAzureMapsXmlService? xmlService) : base(xmlService)
        {
            Buffer?.SetBindMinMax(0, null);
            ClusterMaxZoom?.SetBindMinMax(1, 24);
            ClusterMinPoints?.SetBindMinMax(2, null);
            ClusterRadius?.SetBindMinMax(0, 360);
            MaxZoom?.SetBindMinMax(1, 24);
            Tolerance?.SetBindMinMax(0, 1);
        }

        public List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(Buffer, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Cluster, UIModelInputType.Select, lookup: booleans),
                new UIModelInputValue(ClusterMaxZoom, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(ClusterMinPoints, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(ClusterRadius, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(GenerateId, UIModelInputType.Select, lookup: booleans),
                new UIModelInputValue(LineMetrics, UIModelInputType.Select, lookup: booleans),
                new UIModelInputValue(MaxZoom, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(PromoteId, UIModelInputType.Text),
                new UIModelInputValue(Tolerance, UIModelInputType.Text, TextFieldType.Number),
            };

            return items;
        }

        public new DataSourceOptions? Source { get => base.Source; internal set => base.Source = value; }

        public IUIModelValue Buffer => GetItem(nameof(DataSourceOptions.Buffer))!;
        public IUIModelValue Cluster => GetItem(nameof(DataSourceOptions.Cluster))!;
        public IUIModelValue ClusterMaxZoom => GetItem(nameof(DataSourceOptions.ClusterMaxZoom))!;
        public IUIModelValue ClusterMinPoints => GetItem(nameof(DataSourceOptions.ClusterMinPoints))!;
        public IUIModelValue ClusterRadius => GetItem(nameof(DataSourceOptions.ClusterRadius))!;
        public IUIModelValue GenerateId => GetItem(nameof(DataSourceOptions.GenerateId))!;
        public IUIModelValue LineMetrics => GetItem(nameof(DataSourceOptions.LineMetrics))!;
        public IUIModelValue MaxZoom => GetItem(nameof(DataSourceOptions.MaxZoom))!;
        public IUIModelValue PromoteId => GetItem(nameof(DataSourceOptions.PromoteId))!;
        public IUIModelValue Tolerance => GetItem(nameof(DataSourceOptions.Tolerance))!;
    }
}
