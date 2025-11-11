using Marqdouj.DotNet.AzureMaps.Map.GeoJson;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public class PositionUIModel(IAzureMapsXmlService? xmlService) : XmlUIModel<Position>(xmlService)
    {
        public List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(Longitude, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Latitude, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Elevation, UIModelInputType.Text, TextFieldType.Number),
            };

            return items;
        }

        public IUIModelValue Longitude => GetItem(nameof(Position.Longitude))!;
        public IUIModelValue Latitude => GetItem(nameof(Position.Latitude))!;
        public IUIModelValue Elevation => GetItem(nameof(Position.Elevation))!;
    }
}
