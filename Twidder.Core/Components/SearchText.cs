using CommunityToolkit.Mvvm.ComponentModel;

namespace Twidder.Core.Components;

public sealed partial class SearchText : ObservableObject
{
    private string _oldValue = string.Empty;

    [ObservableProperty]
    private string _text = string.Empty;

    [ObservableProperty]
    private bool _isReadonly = false;

    public string Content
    {
        get => Text;
        set => Text = value;
    }

    public void SetEnabled() => IsReadonly = false;

    public void SetDisabled() => IsReadonly = true;
}
