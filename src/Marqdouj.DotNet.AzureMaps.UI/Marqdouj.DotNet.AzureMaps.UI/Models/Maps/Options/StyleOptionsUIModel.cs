using Marqdouj.DotNet.AzureMaps.Map.Options;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.UI;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps.Options
{
    public class StyleOptionsUIModel(IAzureMapsXmlService? xmlService) : XmlUIModel<StyleOptions>(xmlService), IUIInputListSource
    {
        public virtual List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(ShowFeedbackLink, UIModelInputType.Select, lookup: UILookups.GetBooleans(true)),
                new UIModelInputValue(ShowLabels, UIModelInputType.Select, lookup: UILookups.GetBooleans(true)),
                new UIModelInputValue(ShowLogo, UIModelInputType.Select, lookup: UILookups.GetBooleans(true)),
                new UIModelInputValue(ShowTileBoundaries, UIModelInputType.Select, lookup: UILookups.GetBooleans(true)),
            };

            return items;
        }

        public IUIModelValue ShowFeedbackLink => GetItem(nameof(StyleOptions.ShowFeedbackLink))!;
        public IUIModelValue ShowLabels => GetItem(nameof(StyleOptions.ShowLabels))!;
        public IUIModelValue ShowLogo => GetItem(nameof(StyleOptions.ShowLogo))!;
        public IUIModelValue ShowTileBoundaries => GetItem(nameof(StyleOptions.ShowTileBoundaries))!;
    }
}
