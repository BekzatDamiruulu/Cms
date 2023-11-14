using Cms.Shared.Modules.Image.Services;
using Cms.Shared.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Shared.Modules.Image.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{ 
    private readonly ImageService _service;
    private readonly DataContext _dataContext;
    private DbSet<Entities.Image> Images => _dataContext.Set<Entities.Image>();

    public ImageController(ImageService service, DataContext dataContext)
    {
        _service = service;
        _dataContext = dataContext;
    }
    [HttpPost]
    public async Task<IActionResult> Create(IFormFile file,string alt)
    {
        try
        {
            var imageId = await _service.UploadImage(file, alt);
            return Ok(new {ImageId = imageId});
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("{imageId:long}")]
    public async Task<IActionResult> GetById(long imageId)
    {
        try
        {
            var image =await Images.FirstOrDefaultAsync(i=>i.Id == imageId);
            return File(image.Source, image.MimeType, image.Caption);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(long imageId)
    {
        try
        {
            var image = await Images.FirstOrDefaultAsync(i=>i.Id == imageId);
            Images.Remove(image);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
             return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(IFormFile file,long imageId,string alt)
    {
        try
        {
            var id = await _service.Update(file,imageId,alt);
            return Ok(new {Id=id});
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}