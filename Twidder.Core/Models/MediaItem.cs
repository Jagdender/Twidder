using System;

namespace Twidder.Core.Models
{
    public sealed class MediaItem(Uri url)
    {
        public Uri Url { get; } = url;
    }
}
