using Microsoft.Extensions.Configuration;
using System.Xml.Linq;

namespace Demo07.CustomProvider;

// -----------------------------------------------------------------------
// IConfigurationSource
// Registered with ConfigurationBuilder; creates the provider on demand.
// -----------------------------------------------------------------------
public class XmlConfigurationSource : IConfigurationSource
{
    public string FilePath { get; }

    public XmlConfigurationSource(string filePath) => FilePath = filePath;

    public IConfigurationProvider Build(IConfigurationBuilder builder)
        => new XmlConfigurationProvider(this);
}

// -----------------------------------------------------------------------
// IConfigurationProvider
// Inherits ConfigurationProvider (base class with Data dictionary helpers).
// Only Load() needs to be implemented for a file-based provider.
// -----------------------------------------------------------------------
public class XmlConfigurationProvider : ConfigurationProvider
{
    private readonly XmlConfigurationSource _source;

    public XmlConfigurationProvider(XmlConfigurationSource source)
        => _source = source;

    public override void Load()
    {
        var doc = XDocument.Load(_source.FilePath);
        var root = doc.Root;
        if (root is null)
            return;

        // Flatten XML into colon-separated keys matching IConfiguration convention:
        //   <Database><Host>...</Host></Database>  →  "Database:Host"
        var data = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        Flatten(root, prefix: string.Empty, data);
        Data = data;
    }

    private static void Flatten(XElement element, string prefix, Dictionary<string, string?> data)
    {
        foreach (var child in element.Elements())
        {
            string key = string.IsNullOrEmpty(prefix)
                ? child.Name.LocalName
                : $"{prefix}:{child.Name.LocalName}";

            if (child.HasElements)
                Flatten(child, key, data);   // recurse into nested elements
            else
                data[key] = child.Value;     // leaf — store value
        }
    }
}

// -----------------------------------------------------------------------
// Extension method for fluent builder registration
// -----------------------------------------------------------------------
public static class XmlConfigurationExtensions
{
    public static IConfigurationBuilder AddXmlFile(
        this IConfigurationBuilder builder,
        string filePath)
    {
        return builder.Add(new XmlConfigurationSource(filePath));
    }
}
