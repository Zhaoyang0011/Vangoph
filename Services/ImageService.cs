using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Vangoph.Data;
using Vangoph.Models;

namespace Vangoph.Services;

public class ImageService
{
    private readonly AppDbContext _dbContext;

    public ImageService(AppDbContext context)
    {
        _dbContext = context;
    }

    public async Task UploadImageAsync(Image image, IFormFile file)
    {
        image.CreationDate = DateTime.Now;

        var fileExtension = Path.GetExtension(file.FileName);
        var uniqueFileName = Guid.NewGuid() + fileExtension;

        var relativePath = "uploads/" + uniqueFileName;
        var absPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

        using (var stream = new FileStream(absPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        image.ImageUrl = "/" + relativePath;

        await _dbContext.AddAsync(image);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Image>> GetImagesAsync(int pageNum)
    {
        return await _dbContext.Images.Skip(50 * pageNum)
            .Take(50)
            .ToListAsync();
    }

    public async Task<Image> GetImageByIdAsync(int imageId)
    {
        return await _dbContext.Images.FindAsync(imageId);
    }

    public async Task DeleteImageByIdAsync(int imageId)
    {
        await _dbContext.Images.Where(it => it.Id == imageId).ExecuteDeleteAsync();
    }
}