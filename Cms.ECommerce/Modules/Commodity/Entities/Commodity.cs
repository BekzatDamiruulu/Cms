using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Cms.Shared.Shared;
using Cms.Shared.Shared.Entities;

namespace Cms.ECommerce.Modules.Commodity.Entities;

public class Commodity : Entity
{
    public string Caption { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Weight { get; set; }
    public string UnitOfMass { get; set; } = null!;
    public int AmountOfSet { get; set; }
    public int Price { get; set; }
    public long ImageId { get; set; }
    [JsonIgnore]
    public Cms.Shared.Modules.Image.Entities.Image? Picture { get; set; } 
    protected override int GetClassId() => ClassNames.Commodity;
}