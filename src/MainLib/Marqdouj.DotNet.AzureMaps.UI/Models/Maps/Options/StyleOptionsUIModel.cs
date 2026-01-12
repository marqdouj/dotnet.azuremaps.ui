using Marqdouj.DotNet.AzureMaps.Map.Configuration;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.FluentUI.UIInput;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps.Options
{
    public class StyleOptionsUIModel(IAzureMapsXmlService? xmlService) : XmlUIModel<StyleOptions>(xmlService), IUIInputListSource
    {
        private static readonly List<Option<string>> styles = GetMapStyles();

        private static List<Option<string>> GetMapStyles()
        {
            List<MapStyle> styles = [.. Enum.GetValues<MapStyle>()];
            styles.Remove(Map.Configuration.MapStyle.Blank);
            styles.Remove(Map.Configuration.MapStyle.Blank_Accessible);

            var options = styles.GetEnumLookup(false);
            return options;
        }

        public virtual List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(ShowFeedbackLink, UIModelInputType.Select, lookup: UILookups.GetBooleans(true)),
                new UIModelInputValue(ShowLabels, UIModelInputType.Select, lookup: UILookups.GetBooleans(true)),
                new UIModelInputValue(ShowLogo, UIModelInputType.Select, lookup: UILookups.GetBooleans(true)),
                new UIModelInputValue(ShowTileBoundaries, UIModelInputType.Select, lookup: UILookups.GetBooleans(true)),
                new UIModelInputValue(MapStyle, UIModelInputType.Select, lookup: styles),
            };

            return items;
        }

        public IUIModelValue ShowFeedbackLink => GetItem(nameof(StyleOptions.ShowFeedbackLink))!;
        public IUIModelValue ShowLabels => GetItem(nameof(StyleOptions.ShowLabels))!;
        public IUIModelValue ShowLogo => GetItem(nameof(StyleOptions.ShowLogo))!;
        public IUIModelValue ShowTileBoundaries => GetItem(nameof(StyleOptions.ShowTileBoundaries))!;
        public IUIModelValue MapStyle => GetItem(nameof(StyleOptions.Style))!;
    }
}
