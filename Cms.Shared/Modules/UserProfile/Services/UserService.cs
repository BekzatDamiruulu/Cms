using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Cms.Shared.Modules.UserProfile.Exceptions;
using Cms.Shared.Modules.UserProfile.Models;
using Cms.Shared.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cms.Shared.Modules.UserProfile.Services;


public class UserService 
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly DataContext _dataContext;

    public UserService(UserManager<IdentityUser> userManager, DataContext dataContext, RoleManager<IdentityRole> roleManager) 
    {
        _userManager = userManager;
        _dataContext = dataContext;
        _roleManager = roleManager;
    }
    public async Task<UserManagerResponse> RegisterUserAsync(RegisterModel model)
    {
        var identityUser = new IdentityUser
        {
            Email = model.Email,
            UserName = model.Email,
        };

        await using var transaction = await _dataContext.Database.BeginTransactionAsync();

        try
        {
            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (!result.Succeeded) throw new UserRegistrationException(result.Errors);
            
            model.UserProfile.UserId = identityUser.Id;
            await _dataContext.Set<Entities.UserProfile>().AddAsync(model.UserProfile);
        
            await _dataContext.SaveChangesAsync();
            
            await transaction.CommitAsync();
            
            var userRoles = await _userManager.GetRolesAsync(identityUser);
            var jwt = await GetToken(identityUser, userRoles);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            return new UserManagerResponse
            {
                Token = token,
                Profile = model.UserProfile,
                Roles = userRoles
            };
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception(e.Message, innerException: e.InnerException);
        }
       
    }
    public async Task<UserManagerResponse> LoginUserAsync(LoginModel model)
    {
        
        var user = await _userManager.FindByNameAsync(model.Email);
        if (user == null) throw new NullReferenceException("Пользователь не найден");
        user.SecurityStamp = Guid.NewGuid().ToString();
        await _userManager.UpdateAsync(user);
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!isPasswordValid) throw new ArgumentOutOfRangeException("Пароль не верный");
        
        var userRoles = await _userManager.GetRolesAsync(user);
        var token = await GetToken(user, userRoles);
        
        var userProfile = await _dataContext.Set<Entities.UserProfile>().FirstOrDefaultAsync(up => up.UserId == user.Id);
        if (userProfile == null) throw new NullReferenceException("Профиль для пользователя не определён");

        return new UserManagerResponse()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Profile = userProfile,
            Roles = userRoles
        };
        
    }
    private async Task<JwtSecurityToken> GetToken(IdentityUser user, IEnumerable<string> roles)
    {
        var authSigningKey = new SymmetricSecurityKey("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"u8.ToArray());
        var securityStamp = await _userManager.GetSecurityStampAsync(user);
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.UserData, securityStamp) 
        };
        authClaims.AddRange(roles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
            
        var token = new JwtSecurityToken(
            issuer: "issuer",
            audience: "audience",
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
    public async Task<bool> ResetPasswordAsync(ResetPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) throw new NullReferenceException();
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user,token, model.NewPassword);
        return result.Succeeded;
    }
    public async Task<bool> SetRoleAsync(SetRoleModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null) throw new NullReferenceException();
        var isExist = await _roleManager.RoleExistsAsync(model.RoleName);
        if (!isExist) throw new NullReferenceException("Такой роли не сушествует");
        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
        if (!result.Succeeded) throw new UserRegistrationException(result.Errors);
        return true;
    }
    public async Task<bool> DeleteRoleAsync(SetRoleModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null) throw new NullReferenceException();
        var isExist = await _roleManager.RoleExistsAsync(model.RoleName);
        if (!isExist) throw new NullReferenceException("Такой роли не сушествует");
        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
        if (!result.Succeeded) throw new UserRegistrationException(result.Errors);
        return true;
    }
}