using Microsoft.AspNetCore.Components.Forms;

namespace BookMyHome.UI.Services
{
    public class ImageFileService : IImageFileService
    {
        private static readonly string[] DefaultAllowed = new[] { "image/jpeg", "image/png" };

        public async Task<ImageProcessResult> ProcessAsync(IBrowserFile file, long maxBytes = 150_000, string[]? allowedContentTypes = null)
        {
            if (file is null) return ImageProcessResult.Fail("No file provided.");

            var allowed = allowedContentTypes is { Length: > 0 } ? allowedContentTypes : DefaultAllowed;

            if (!allowed.Contains(file.ContentType, StringComparer.OrdinalIgnoreCase))
            {
                return ImageProcessResult.Fail($"Unsupported file type: {file.ContentType}. Allowed: {string.Join(", ", allowed)}");
            }

            if (file.Size > maxBytes)
            {
                return ImageProcessResult.Fail($"Image too large. Max {maxBytes / 1000}KB");
            }

            using var stream = new MemoryStream();
            await file.OpenReadStream(maxAllowedSize: maxBytes).CopyToAsync(stream);
            var bytes = stream.ToArray();

            var dataUrl = $"data:{file.ContentType};base64,{Convert.ToBase64String(bytes)}";
            return ImageProcessResult.Ok(bytes, dataUrl);
        }
    }
}
