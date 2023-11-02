using Cms.Shared.Shared;
using Cms.Shared.Shared.Entities;

namespace Cms.Shared.Modules.Image.Entities;

public class Image : Entity
{
    protected override int GetClassId() => ClassNames.Picture;
    public string Caption { get; set; }
    public long Size { get; set; }
    public string SizeType { get; set; }
    public string MimeType { get; set; }
    public byte[] Source { get; set; }
    public string Extension { get; set; }
    public string Alt { get; set; }
}


