using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Twidder.Core.Components;
using Twidder.Core.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Twidder.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [INotifyPropertyChanged]
    public sealed partial class MediaPage : Page
    {
        private readonly FetchEngine _engine;

        internal ObservableCollection<MediaItem> Items = [];
        internal SearchText Search { get; }

        public MediaPage(SearchText searchText, FetchEngine engine)
        {
            this.InitializeComponent();
            Search = searchText;
            _engine = engine;
        }

        private async void Submit(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            string target = Search.Content;
            Search.SetDisabled();
            Search.Content = "Loading...";
            await foreach (var item in _engine.PullAsync(target))
            {
                await Task.Delay(200);
                Items.Add(item);
            }
            Search.Content = "Success";
            await Task.Delay(1000);
            Search.SetEnabled();
            Search.Content = string.Empty;
        }
    }
}
