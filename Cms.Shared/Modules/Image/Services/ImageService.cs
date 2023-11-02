using Cms.Shared.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Shared.Modules.Image.Services;

public class ImageService
{
    private readonly DataContext _dataContext;

    public ImageService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<long> UploadImage(IFormFile? file,string alt)
    {
        if (file is { Length: > 0 })
        {
            using MemoryStream memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var extension = Path.GetExtension(file.FileName);
            var image = new Cms.Shared.Modules.Image.Entities.Image()
            {
                Caption = file.FileName,
                Size = file.Length, SizeType = "bytes",
                MimeType = "image/" + extension.Replace(".", ""),
                Source = memoryStream.ToArray(),
                Extension = extension, Alt = alt
            };
            _dataContext.Set<Cms.Shared.Modules.Image.Entities.Image>().Add(image);
            await _dataContext.SaveChangesAsync();
            return image.Id;
        }
        return 0;
    }
    
    public async Task<long> Update(IFormFile file,long imageId,string alt)
    {
        if (file is { Length: > 0 })
        {
            using MemoryStream memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var image =await _dataContext.Set<Entities.Image>().FirstOrDefaultAsync(i=>i.Id == imageId);
            if (image == null) throw new NullReferenceException();
            image.Source = memoryStream.ToArray();
            image.Alt = alt;
            image.MimeType = "image/" + Path.GetExtension(file.FileName).Replace(".", "");
            image.Extension = Path.GetExtension(file.FileName);
            await _dataContext.SaveChangesAsync();
            return image.Id;
        }
        throw new Exception("upload file");
    }
   
}