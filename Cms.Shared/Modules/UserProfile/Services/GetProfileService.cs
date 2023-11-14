using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Cms.Shared.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Shared.Modules.UserProfile.Services;

public class GetProfileService
{
    private readonly HttpContext? _context;
    private readonly DataContext _dataContext;

    public GetProfileService( IHttpContextAccessor context, DataContext dataContext)
        {
            _context = context.HttpContext;
            _dataContext = dataContext;
        }
    
    public object GetProfile()
    {
        var userName = _context?.User.Identity?.Name;
        var user = _dataContext.Set<IdentityUser>().FirstOrDefault(u=>u.UserName == userName);
        if (user == null) throw new NullReferenceException("user is null");
        var profile = _dataContext.Set<Entities.UserProfile>().FirstOrDefault(pro=>pro.UserId == user.Id);
        if(profile == null)throw new NullReferenceException("profile is null");
        return new {Email = userName,Profile=profile};
    }
}