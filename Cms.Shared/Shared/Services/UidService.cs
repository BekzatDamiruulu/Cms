using Microsoft.AspNetCore.Http;

namespace Cms.Shared.Shared.Services;

public class UidService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UidService( IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUid()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) throw new NullReferenceException("HttpContext не доступен");
        
        var uid = Guid.Empty;
        var uidCookie = context.Request.Cookies["_uid"];
        if (uidCookie != null)
        {
            if (Guid.TryParse(uidCookie, out var tryResult))
            {
                uid = tryResult;
            }
        }

        if (uid != Guid.Empty) return uid;
        
        
        uid = Guid.NewGuid();
        var options = new CookieOptions
        {
            Expires = DateTime.Now.AddYears(1)
        };
        context.Response.Cookies.Append("_uid", uid.ToString(), options);
        return uid;
    }
}