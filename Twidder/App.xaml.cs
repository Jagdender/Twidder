using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Twidder.Core.Components;
using Twidder.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Twidder
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        public static MainWindow Window { get; private set; }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            var services = await ConfigureServicesAsync();

            var mainWindow = services.GetRequiredService<MainWindow>();
            Window = mainWindow;
            mainWindow.Activate();
        }

        private async Task<IServiceProvider> ConfigureServicesAsync()
        {
            var collection = new ServiceCollection();

            var engine = await FetchEngine.CreateAsync();

            collection.AddSingleton(engine);

            collection.AddSingleton<MainWindow>();
            collection.AddSingleton<LoginPage>();
            collection.AddSingleton<MediaPage>();
            collection.AddSingleton<Navigator>();
            collection.AddSingleton<SearchText>();

            return collection.BuildServiceProvider();
        }
    }
}
