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
    
    public object? GetProfile()
    {
        try
        {
            string? jwtToken = _context?.Request.Headers["Authorization"];
            if(jwtToken == null)  throw new Exception("token is null");
            jwtToken = jwtToken.Replace("Bearer ", string.Empty);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;
            if (jsonToken != null)
            {
                var claims =  jsonToken.Claims;
                var name = claims.FirstOrDefault(c=>c.Type == ClaimTypes.Name)?.Value;
                if (name == null) throw new Exception("name is null");
                var user = _dataContext.Set<IdentityUser>().FirstOrDefault(u=>u.UserName == name);
                if (user == null) throw new NullReferenceException("user is null");
                var profile = _dataContext.Set<Entities.UserProfile>().FirstOrDefault(pr => pr.UserId == user.Id);
                if(profile ==null)  throw new NullReferenceException("profile is null");
                return new { Email = name, Profile=profile};
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
        return null;
    }
}