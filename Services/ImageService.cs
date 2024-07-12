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

        var filePath = Path.Combine("uploads", uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        image.ImageUrl = filePath;

        await _dbContext.AddAsync(image);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Image>> GetImagesAsync()
    {
        return await _dbContext.Images.ToListAsync();
    }
}