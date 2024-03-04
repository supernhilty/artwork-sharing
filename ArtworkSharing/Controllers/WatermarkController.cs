using System;
using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WatermarkController : ControllerBase
	{
		private readonly Cloudinary _cloudinary;

		public WatermarkController()
		{
			Account account = new Account(
				"cloud_name",
				"api_key",
				"api_secret"
			);
			_cloudinary = new Cloudinary(account);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddWatermark(IFormFile file)
		{
			try
			{
				if (file == null || file.Length == 0)
					return BadRequest("No file uploaded.");

				byte[] imageData;
				using (var stream = new MemoryStream())
				{
					await file.CopyToAsync(stream);
					imageData = stream.ToArray();
				}

				var uploadParams = new ImageUploadParams()
				{
					File = new FileDescription(file.FileName, new MemoryStream(imageData)),
					PublicId = "watermarked_image",
					Overwrite = true,
					Transformation = new Transformation().Overlay("watermark_image").Opacity(50)
				};

				var uploadResult = await _cloudinary.UploadAsync(uploadParams);

				return Ok(uploadResult);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPost("remove")]
		public async Task<IActionResult> RemoveWatermark(IFormFile file)
		{
			try
			{
				if (file == null || file.Length == 0)
					return BadRequest("No file uploaded.");

				byte[] imageData;
				using (var stream = new MemoryStream())
				{
					await file.CopyToAsync(stream);
					imageData = stream.ToArray();
				}

				var uploadParams = new ImageUploadParams()
				{
					File = new FileDescription(file.FileName, new MemoryStream(imageData)),
					PublicId = "watermark_removed_image", 
					Overwrite = true,
					Transformation = new Transformation().Effect("remove_watermark")
				};

				var uploadResult = await _cloudinary.UploadAsync(uploadParams);

				return Ok(uploadResult);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
