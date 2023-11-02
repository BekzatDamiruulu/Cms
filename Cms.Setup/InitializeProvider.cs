using Cms.Shared;
using Cms.Shared.Shared;

namespace Cms.Setup;

public class InitializeProvider
{
    private readonly IEnumerable<IInitializer> _initializers;

    public InitializeProvider(IEnumerable<IInitializer> initializers)
    {
        _initializers = initializers;
    }

    public async Task Initialize()
    {
        foreach (var initializer in _initializers)
        {
            await initializer.Initialize();
        }
    }
}