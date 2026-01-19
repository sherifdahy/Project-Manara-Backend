using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace App.Infrastructure.Localization.Localizers;

public class JsonStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly IDistributedCache _cache;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public JsonStringLocalizerFactory(IDistributedCache cache, IWebHostEnvironment webHostEnvironment)
    {
        _cache = cache;
        _webHostEnvironment = webHostEnvironment;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        return new JsonStringLocalizer(_cache, _webHostEnvironment);
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return new JsonStringLocalizer(_cache, _webHostEnvironment);
    }
}
