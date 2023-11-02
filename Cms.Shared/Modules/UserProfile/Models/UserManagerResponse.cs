namespace Cms.Shared.Modules.UserProfile.Models;

public class UserManagerResponse
{
    public string? Token { get; set; }
    public ICollection<string> Roles { get; set; } = new List<string>();
    public ICollection<string> Errors { get; set; } = new List<string>();
    public Entities.UserProfile? Profile { get; set; }
}

public class WhatsAppMessage
{
    public string? Token { get; set; }
    public string? ProfileId { get; set; }
    public string? To { get; set; }
    public string? Text { get; set; }
}

public class MailingModel
{
    public long ClinicId { get; set; }
    public string Text { get; set; }
}