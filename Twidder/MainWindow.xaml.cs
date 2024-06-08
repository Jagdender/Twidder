using Twidder.Pages;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Twidder
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WindowEx
    {
        internal Navigator Navigator { get; }

        public MainWindow(Navigator navigator)
        {
            this.InitializeComponent();

            Width = 1000;
            Height = 618;

            Navigator = navigator;
            Navigator.NavigateTo<LoginPage>();
        }
    }
}
