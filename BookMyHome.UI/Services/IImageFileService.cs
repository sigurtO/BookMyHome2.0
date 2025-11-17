using Microsoft.AspNetCore.Components.Forms;

namespace BookMyHome.UI.Services
{
    public interface IImageFileService
    {
        Task<ImageProcessResult> ProcessAsync(IBrowserFile file, long maxBytes = 150_000, string[]? allowedContentTypes = null);
    }

    public sealed class ImageProcessResult
    {
        public bool Success { get; init; }
        public byte[]? Bytes { get; init; }
        public string? DataUrl { get; init; }
        public string? Error { get; init; }

        public static ImageProcessResult Ok(byte[] bytes, string dataUrl) => new()
        {
            Success = true,
            Bytes = bytes,
            DataUrl = dataUrl
        };

        public static ImageProcessResult Fail(string error) => new()
        {
            Success = false,
            Error = error
        };
    }
}
