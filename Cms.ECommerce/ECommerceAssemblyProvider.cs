using System.Reflection;

namespace Cms.ECommerce;

public static class ECommerceAssemblyProvider
{
    public static Assembly GetECommerceAssembly()
    {
        return Assembly.GetAssembly(typeof(ECommerceAssemblyProvider))!;
    }
}