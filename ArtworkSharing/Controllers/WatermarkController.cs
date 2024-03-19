﻿using ArtworkSharing.Core.Interfaces.Services;
using ArtworkSharing.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WatermarkController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IFireBaseService _fireBaseService;

    public WatermarkController(IHttpClientFactory clientFactory, IFireBaseService fireBaseService)
    {
        _clientFactory = clientFactory;
        _fireBaseService = fireBaseService;
    }

    [HttpPost]
    public async Task<IActionResult> PostWatermarkAsync([FromBody] WatermarkRequestModel model)
    {
        try
        {
            // Prepare the request body
            var request = new
            {
                mainImageUrl = model.MainImageUrl,
                markImageUrl = model.MarkImageUrl,
                markRatio = model.MarkRatio,
                opacity = model.Opacity,
                position = model.Position,
                positionX = model.PositionX,
                positionY = model.PositionY,
                margin = model.Margin
            };

            // Send POST request to QuickChart Watermark API
            var client = _clientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://quickchart.io/watermark", request);

            // Check if request was successful
            response.EnsureSuccessStatusCode();

            // Read response content as byte array
            var imageBytes = await response.Content.ReadAsByteArrayAsync();

            // Call the FireBaseService to upload the watermarked image and get the image link
            var imageUrl = await _fireBaseService.UploadImageSingleNotList(imageBytes, "jpeg");

            // Return the image link
            return Ok(new { imageUrl });
        }
        catch (Exception)
        {
            return StatusCode(500); // Internal Server Error
        }
    }
}

// Model to represent the request body for watermarking
public class WatermarkRequestModel
{
    public string MainImageUrl { get; set; }
    public string MarkImageUrl { get; set; }
    public double MarkRatio { get; set; }
    public double Opacity { get; set; }
    public string Position { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int Margin { get; set; }
}