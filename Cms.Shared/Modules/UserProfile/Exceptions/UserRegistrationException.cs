using Microsoft.AspNetCore.Identity;

namespace Cms.Shared.Modules.UserProfile.Exceptions;

[Serializable]
public class UserRegistrationException : Exception
{
    public IEnumerable<IdentityError> Errors { get; set; } 
    public UserRegistrationException(IEnumerable<IdentityError> errors) : base($"Есть ошибки не возможно зарегистрироваться")
    {
        Errors = errors;
    }
}