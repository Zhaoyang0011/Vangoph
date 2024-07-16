using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vangoph.Models;
using Vangoph.Services;

namespace Vangoph.Controllers;

[Route("[controller]")]
public class ImageController : Controller
{
    private readonly ImageService _imageService;

    public ImageController(ImageService imageService)
    {
        _imageService = imageService;
    }

    public async Task<IActionResult> Index()
    {
        List<Image> images = await _imageService.GetImagesAsync(0);
        return View("Index", images);
    }

    [HttpGet("detail")]
    public async Task<IActionResult> DetailAsync(int imageId)
    {
        var image = await _imageService.GetImageByIdAsync(imageId);
        if (image != null)
            return View("ImageDetail", image);
        return View("Error", "Image does not exists!");
    }

    [HttpGet("upload")]
    public IActionResult UploadPage()
    {
        return View("ImageUpload");
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadAsync(Image image, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return View("ImageUpload", "No file selected.");
        }

        await _imageService.UploadImageAsync(image, file);
        return Redirect("/Image");
    }

    [HttpGet("manage")]
    public async Task<IActionResult> ManageAsync()
    {
        List<Image> images = await _imageService.GetImagesAsync(0);
        return View("ImageManage", images);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteAsync(int imageId)
    {
        await _imageService.DeleteImageByIdAsync(imageId);
        return Redirect("manage");
    }

    [HttpGet("update")]
    public async Task<IActionResult> UpdatePageAsync(int imageId)
    {
        var image = await _imageService.GetImageByIdAsync(imageId);
        if (image != null)
            return View("ImageEdit", image);
        return View("Error", "Image does not exists!");
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(Image image)
    {
        await _imageService.UpdateImageAsync(image);
        return Redirect($"detail?imageId={image.Id}");
    }
}