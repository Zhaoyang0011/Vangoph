using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Vangoph.Models;
using Vangoph.Services;

namespace Vangoph.Controllers;

public class ImageController : Controller
{
    private readonly ImageService _imageService;

    public ImageController(ImageService imageService)
    {
        _imageService = imageService;
    }

    public IActionResult ImageUpload()
    {
        return View();
    }

    public async Task<IActionResult> DetailAsync(int imageId)
    {
        var image = await _imageService.GetImageByIdAsync(imageId);
        return View("ImageDetail",image);
    }

    public async Task<IActionResult> Index()
    {
        List<Image> images = await _imageService.GetImagesAsync(0);
        return View(images);
    }

    public async Task<IActionResult> UploadAsync(Image image, IFormFile file)
    {
        if (file == null)
        {
            return View("ImageUpload", "No file selected.");
        }

        await _imageService.UploadImageAsync(image, file);
        return await Index();
    }
}