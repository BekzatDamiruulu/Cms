using Cms.Shared.Modules.UserProfile.Entities;
using Cms.Shared.Modules.UserProfile.Models;
using Cms.Shared.Modules.UserProfile.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cms.Shared.Shared;

public class SharedInitializer : IInitializer
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DataContext _dataContext;
    private readonly UserService _userService;

    public SharedInitializer(DataContext dataContext, RoleManager<IdentityRole> roleManager, UserService userService, UserManager<IdentityUser> userManager)
    {
        _dataContext = dataContext;
        _roleManager = roleManager;
        _userService = userService;
        _userManager = userManager;
    }

    public async Task Initialize()
    {
        var role = new IdentityRole
        {
            Name = "Administrator",
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
        
        var administrator = await _roleManager.Roles.ToListAsync();
        if (!administrator.Exists(r => r.Name == "Administrator"))
        {
            await _roleManager.CreateAsync(role);
            await _dataContext.SaveChangesAsync();
        }

        var registerModel = new RegisterModel()
        {
            Email = "bekzat@gmail.com",
            Password = "Bekzat2003.",
            UserProfile = new UserProfile
            {
                FirstName = "Bekzat",
                LastName = "Damirbek",
            }
        };
        var user = await _userManager.FindByEmailAsync(registerModel.Email);
        
        if (user == null)
        {
            await _userService.RegisterUserAsync(registerModel);
            user = await _userManager.FindByEmailAsync(registerModel.Email);
            if (user == null) throw new Exception("Ошибка при регистрации пользователя");
            await _userManager.AddToRoleAsync(user, role.Name);
            await _dataContext.SaveChangesAsync();
        }
    }
}