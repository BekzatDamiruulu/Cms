using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Cms.Shared.Shared;
using Cms.Shared.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace Cms.Shared.Modules.UserProfile.Entities;

public class UserProfile: Entity
{
    private string? _fullName { get; set; }
    [StringLength(200)] 
    public string FirstName { get; set; } = "";
    [StringLength(200)] 
    public string LastName { get; set; } = "";
    public string? Patronymic { get; set; }
    public string PhoneNumber { get; set; } = "";
    public string WhatsAppNumber { get; set; } = "";
    public string Address { get; set; } = "";
    public string Description { get; set; } = "";

    public string FullName
    {
        get => !string.IsNullOrEmpty(_fullName) ? _fullName : $"{LastName} {FirstName} {Patronymic}";
        set => _fullName = value;
    }

    public string InformationSource { get; set; } = "";
    public string? UserId { get; set; }
    [JsonIgnore]
    public IdentityUser? User { get; set; }
    public DateTime BirthDate { get; }= DateTime.Now;
    public IEnumerable<string> Roles { get; set; } = new List<string>();
    public JsonDocument? Data { get; set; }
    protected override int GetClassId() => ClassNames.UserProfile;
}