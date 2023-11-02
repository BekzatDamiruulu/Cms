using System.ComponentModel.DataAnnotations;

namespace Cms.Shared.Modules.UserProfile.Models;

public class ResetPasswordModel
{
    [Required]
    public string Token { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string NewPassword { get; set; }

}