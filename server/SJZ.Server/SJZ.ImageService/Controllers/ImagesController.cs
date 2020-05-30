using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SJZ.Images.Repository;

namespace SJZ.ImageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(
            IImageRepository repository,
            ILogger<ImagesController> logger)
        {
            _imageRepository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetImageListAsync([FromQuery] string owner, [FromQuery] int year, [FromQuery] int month)
        {
            try
            {
                var results = await _imageRepository.GetImagesByUserAsnyc(owner, year, month);
                return Ok(results.Select(x => x.AbsoluteUri));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync([FromQuery] string owner, [FromQuery] int year, [FromQuery] int month)
        {
            var file = HttpContext.Request.Form.Files["file"];
            if (file == null)
                file = HttpContext.Request.Form.Files[0];

            var thumbnailDivideBy = GetThumbnailDivideBy(file.Length);

            Uri uri = null;
            Uri thumbUri = null;

            try
            {
                using (var source = file.OpenReadStream())
                {
                    var filename = Guid.NewGuid().ToString().Replace("-", string.Empty) + Path.GetExtension(file.FileName);
                    uri = await _imageRepository.UploadImageAsync(owner, year, month,
                        filename,
                        source);

                    source.Position = 0;

                    using var thumbStream = new MemoryStream();
                    using var image = Image.FromStream(source);
                    var thumb = image.GetThumbnailImage(image.Width / thumbnailDivideBy, image.Height / thumbnailDivideBy,
                        () => false, IntPtr.Zero);
                    thumb.Save(thumbStream, image.RawFormat);
                    thumbUri = await _imageRepository.UploadImageAsync(owner, year, month, $"_thumbnail/{filename}", thumbStream);
                }

                return Ok(new { Uri = uri, Thumbnail = thumbUri });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{url}")]
        public async Task<IActionResult> DeleteAsync(string url)
        {
            var decoded = HttpUtility.UrlDecode(url);
            var containerUrl = new Uri(decoded).GetLeftPart(UriPartial.Authority) + "/pictures/";

            string main, thumb;
            if (decoded.Contains("/_thumbnail"))
            {
                main = decoded.Replace("/_thumbnail", string.Empty).Substring(containerUrl.Length);
                thumb = decoded.Substring(containerUrl.Length);
            }
            else
            {
                main = decoded.Substring(containerUrl.Length);
                thumb = decoded.Insert(decoded.LastIndexOf('/'), "/_thumbnail").Substring(containerUrl.Length);
            }

            await _imageRepository.DeleteAsync(main);
            await _imageRepository.DeleteAsync(thumb);
            return NoContent();
        }

        private static int GetThumbnailDivideBy(long size)
        {
            if (size > 2_000_000)
                return 10;

            if (size > 1_000_000)
                return 5;

            if (size > 5_00_000)
                return 2;

            return 1;
        }
    }
}
