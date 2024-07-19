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
    private readonly MailSender _mailSender;

    public ImageController(ImageService imageService, MailSender mailSender)
    {
        _imageService = imageService;
        _mailSender = mailSender;
    }

    public async Task<IActionResult> Index()
    {
        List<Image> images = await _imageService.GetImagesAsync(0);
        return View(images);
    }

    [HttpGet("detail")]
    public async Task<IActionResult> DetailAsync(int imageId)
    {
        var image = await _imageService.GetImageByIdAsync(imageId);
        if (image != null)
            return View("ImageDetail", image);
        return View("Error", new List<string>() { "Image does not exists!" });
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
        return Redirect("/Image/Manage");
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
        return View("Error", new List<string>() { "Image shold be uploaded!" });
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(Image image)
    {
        await _imageService.UpdateImageAsync(image);
        return Redirect("manage");
    }
}