using Marqdouj.DotNet.AzureMaps.UI.Components;
using Marqdouj.DotNet.AzureMaps.UI.Models.Input;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Marqdouj.DotNet.AzureMaps.UI.Models
{
    public static class DialogExtentions
    {
        public static DialogParameters GetDefaultDialogParameters(string title, string? width = "80%")
        {
            DialogParameters parameters = new()
            {
                Title = title,
                PrimaryAction = "OK",
                PrimaryActionEnabled = true,
                SecondaryAction = "Cancel",
                Width = width,
                TrapFocus = true,
                Modal = false,
                PreventScroll = true
            };

            return parameters;
        }

        public static async Task<DialogResult> ShowInputsDialog(this IDialogService service, string title, List<IUIModelInputValue> inputs, DialogParameters? parameters = null)
        {
            parameters ??= GetDefaultDialogParameters(title);
            IDialogReference dialog = await service.ShowDialogAsync<UIModelInputDialog>(inputs, parameters);
            var result = await dialog.Result;

            return result;
        }

        public static async Task<DialogResult> ShowInputsDialog(this IDialogService service, string title, IUIInputListSource input, DialogParameters? parameters = null)
        {
            parameters ??= GetDefaultDialogParameters(title);
            IDialogReference dialog = await service.ShowDialogAsync<UIModelInputDialog>(input.ToUIInputList(), parameters);
            var result = await dialog.Result;

            return result;
        }
    }
}
