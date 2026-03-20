using Advocu.WebApp.Extensions;

namespace Advocu.WebApp;

/// <summary>
/// Provides data services for the Advocu web application.
/// </summary>
public class AdvocuDataService
{
    /// <summary>
    /// Retrieves a list of tags.
    /// </summary>
    /// <returns>A list of allowed tags.</returns>
    public List<AdvocuTag> GetTags()
    {
        return Enum.GetValues<AdvocuTag>()
            .OrderBy(t => t.GetDisplayName())
            .ToList();
    }
}
