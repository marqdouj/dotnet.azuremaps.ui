using Marqdouj.DotNet.Web.Components.FluentUI.UIInput;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    internal static class UILookups
    {
        //Converts List<Option<string?>> to List<Option<string>> with default null option
        public static List<Option<string>> GetBooleans(bool addDefault, string defaultText = "", string defaultValue = "") =>
            [.. UIExtensions.GetBoolLookup(addDefault, defaultText, defaultValue).Select(e => new Option<string>() { Text = e.Text, Value = e.Value })];
    }
}
