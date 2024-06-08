using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Twidder
{
    public sealed partial class Navigator(IServiceProvider services) : ObservableObject
    {
        private readonly IServiceProvider _services = services;

        [ObservableProperty]
        private Page _currentPage;

        public void NavigateTo<T>()
            where T : Page
        {
            var page = _services.GetRequiredService<T>();
            CurrentPage = page;
        }
    }
}
