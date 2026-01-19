using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace App.Infrastructure.Localization.Localizers;
public class JsonStringLocalizer : IStringLocalizer
{
    private readonly IDistributedCache _cache;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly JsonSerializer _serializer = new();

    public JsonStringLocalizer(IDistributedCache cache, IWebHostEnvironment webHostEnvironment)
    {
        _cache = cache;
        _webHostEnvironment = webHostEnvironment;
    }

    public LocalizedString this[string name]
        => new LocalizedString(name, GetString(name));

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var raw = this[name];
            return new LocalizedString(name, string.Format(raw.Value, arguments));
        }
    }

    public LocalizedString this[string name, string fileFolder]
    {
        get
        {
            var value = GetString(name, fileFolder);
            return new LocalizedString(name, value);
        }
    }



    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return GetAllStrings(null);
    }

    public IEnumerable<LocalizedString> GetAllStrings(string? folder = null)
    {
        var culture = CultureInfo.CurrentCulture.Name;

        var path = GetFullPath(culture, folder);

        using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        using StreamReader streamReader = new(stream, Encoding.UTF8);
        using JsonTextReader reader = new(streamReader);

        while (reader.Read())
        {
            if (reader.TokenType != JsonToken.PropertyName)
                continue;

            var key = reader.Value as string;
            reader.Read();
            var value = _serializer.Deserialize<string>(reader);

            yield return new LocalizedString(key, value);
        }
    }


    public string GetString(string key, string? folder = null)
    {
        var culture = CultureInfo.CurrentCulture.Name;

        var fullFilePath = GetFullPath(culture, folder);

        if (!File.Exists(fullFilePath))
            return string.Empty;

        var cacheKey = $"locale_{culture}_{folder}_{key}";

        var cacheValue = _cache.GetString(cacheKey);
        if (!string.IsNullOrEmpty(cacheValue))
            return cacheValue;

        var value = GetValueFromJson(key, fullFilePath);

        if (!string.IsNullOrEmpty(value))
            _cache.SetString(cacheKey, value);

        return value;
    }

    private string GetFullPath(string culture, string? folder)
    {
        var basePath = _webHostEnvironment.ContentRootPath;

        return string.IsNullOrEmpty(folder)
            ? Path.Combine(basePath, "Resources", $"{culture}.json")
            : Path.Combine(basePath, "Resources", folder, $"{culture}.json");
    }
    private string GetValueFromJson(string propertyName, string filePath)
    {
        using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using StreamReader streamReader = new(stream, Encoding.UTF8);
        using JsonTextReader reader = new(streamReader);

        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.PropertyName &&
                reader.Value as string == propertyName)
            {
                reader.Read();
                return _serializer.Deserialize<string>(reader);
            }
        }

        return string.Empty;
    }
}
