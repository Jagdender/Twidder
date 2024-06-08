using System;
using System.Text.Json;
using Microsoft.UI.Xaml.Controls;
using Twidder.Core.Components;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Twidder.Pages;

public sealed partial class LoginPage : Page
{
    private static readonly JsonSerializerOptions _options =
        new() { PropertyNameCaseInsensitive = true };
    private readonly Navigator _navigator;
    private readonly FetchEngine _engine;

    public LoginPage(Navigator navigator, FetchEngine engine)
    {
        this.InitializeComponent();
        _navigator = navigator;
        _engine = engine;
    }

    private async void ReadFile(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var picker = OpenFilePicker();
        var file = await picker.PickSingleFileAsync();
        string json = await FileIO.ReadTextAsync(file);

        var content = JsonSerializer.Deserialize<AuthInfo>(json, _options);

        await _engine.InitAuthInfoAsync(content.AuthToken, content.Twid);

        _navigator.NavigateTo<MediaPage>();
    }

    private FileOpenPicker OpenFilePicker()
    {
        var picker = new FileOpenPicker() { FileTypeFilter = { ".txt", ".json" } };
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.Window);
        WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

        return picker;
    }

    private class AuthInfo
    {
        public string AuthToken { get; set; }
        public string Twid { get; set; }
    }
}
