using System.Reflection;

namespace Cms.Shared.Shared;

public static class SharedAssembly
{
    public static Assembly GetSharedAssembly()
    {
        return Assembly.GetAssembly(typeof(SharedAssembly))!;
    }
}