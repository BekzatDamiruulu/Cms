using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Cms.Shared.Shared;
using Cms.Shared.Shared.Entities;

namespace Cms.ECommerce.Modules.Category.Entities;

public class Category : Entity
{
    public string Caption { get; set; }= null!;
    public string Description { get; set; }= null!;
    protected override int GetClassId() => ClassNames.Category;
}