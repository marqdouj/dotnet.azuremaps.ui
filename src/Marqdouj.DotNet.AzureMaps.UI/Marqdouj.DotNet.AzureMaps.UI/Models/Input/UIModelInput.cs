using Marqdouj.DotNet.Web.Components.FluentUI.Common;
using Marqdouj.DotNet.Web.Components.UI;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Input
{
    public enum UIModelInputType
    {
        Color,
        Pixel,
        Select,
        Text,
    }

    public interface IUIModelInputValue
    {
        TextFieldType FieldType { get; }
        GridItemOptions GridItemOptions { get; }
        UIModelInputType InputType { get; }
        List<Option<string>>? Lookup { get; }
        IUIModelValue Model { get; }
        int SortOrder { get; set; }
        string? Style { get; set; }
        bool Visible { get; set; }
        string Width { get; set; }
    }

    public class UIModelInputValue(
        IUIModelValue model,
        UIModelInputType inputType,
        TextFieldType? fieldType = null,
        List<Option<string>>? lookup = null,
        GridItemOptions? gridItemOptions = null) : IUIModelInputValue
    {
        public IUIModelValue Model { get; } = model;
        public UIModelInputType InputType { get; } = inputType;
        public TextFieldType FieldType { get; } = fieldType ?? TextFieldType.Text;
        public List<Option<string>>? Lookup { get; } = lookup;
        public GridItemOptions GridItemOptions { get; } = gridItemOptions ?? new();
        public int SortOrder { get; set; } = model.SortOrder;
        public string? Style { get; set; }
        public bool Visible { get; set; } = model.Visible;
        public string Width { get; set; } = "";
    }

}
