using Marqdouj.DotNet.AzureMaps.Map.Common;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Marqdouj.DotNet.AzureMaps.UI.Services;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public class PixelUIModel(IAzureMapsXmlService? xmlService) : XmlUIModel<Pixel>(xmlService)
    {
        public List<IUIModelInputValue> ToUIInputList()
        {
            var items = new List<IUIModelInputValue>
            {
                new UIModelInputValue(X, UIModelInputType.Text, TextFieldType.Number),
                new UIModelInputValue(Y, UIModelInputType.Text, TextFieldType.Number),
            };

            return items;
        }

        public IUIModelValue X => GetItem(nameof(Pixel.X))!;
        public IUIModelValue Y => GetItem(nameof(Pixel.Y))!;
    }
}
