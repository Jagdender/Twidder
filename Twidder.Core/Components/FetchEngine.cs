using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Twidder.Core.Models;

namespace Twidder.Core.Components
{
    public sealed class FetchEngine : IAsyncDisposable
    {
        private readonly IPlaywright _engine;
        private readonly IBrowserContext _context;
        private readonly IPage _page;

        private FetchEngine(IPlaywright engine, IBrowserContext context, IPage page)
        {
            _context = context;
            _engine = engine;
            _page = page;
        }

        public static async Task<FetchEngine> CreateAsync()
        {
            var playWright = await Playwright.CreateAsync();

            var browser = await playWright.Chromium.LaunchAsync(
                new()
                {
                    Channel = "msedge",
                    Headless = true,
                    Proxy = new() { Server = "localhost:7890" }
                }
            );

            var context = await browser.NewContextAsync(new() { BaseURL = "https://x.com" });

            var page = await context.NewPageAsync();

            return new FetchEngine(playWright, context, page);
        }

        public static async Task<FetchEngine> CreateAsync(string authToken, string twid)
        {
            var playWright = await Playwright.CreateAsync();

            var browser = await playWright.Chromium.LaunchAsync(
                new()
                {
                    Channel = "msedge",
                    Headless = true,
                    Proxy = new() { Server = "localhost:7890" }
                }
            );

            var context = await browser.NewContextAsync(new() { BaseURL = "https://x.com" });

            await context.AddCookiesAsync(
                [
                    new()
                    {
                        Url = "https://x.com",
                        Name = "auth_token",
                        Value = authToken
                    },
                    new()
                    {
                        Url = "https://x.com",
                        Name = "twid",
                        Value = twid
                    }
                ]
            );

            var page = await context.NewPageAsync();

            return new FetchEngine(playWright, context, page);
        }

        public async Task InitAuthInfoAsync(string authToken, string twid)
        {
            await _context.ClearCookiesAsync();
            await _context.AddCookiesAsync(
                [
                    new()
                    {
                        Url = "https://x.com",
                        Name = "auth_token",
                        Value = authToken
                    },
                    new()
                    {
                        Url = "https://x.com",
                        Name = "twid",
                        Value = twid
                    }
                ]
            );
        }

        public async IAsyncEnumerable<MediaItem> PullAsync(string authorId)
        {
            await _page.GotoAsync(
                $"https://x.com/{authorId}/media",
                new() { WaitUntil = WaitUntilState.NetworkIdle }
            );

            var listitems = await _page.GetByRole(AriaRole.Listitem).AllAsync();

            var images = listitems.Select(item => item.Locator("img").First);

            foreach (var image in images)
            {
                var src = await image.GetAttributeAsync("src");
                yield return new(new(src));
            }
        }

        public async ValueTask DisposeAsync()
        {
            await _context.CloseAsync();
            await _context.DisposeAsync();
            _engine.Dispose();
        }
    }
}
