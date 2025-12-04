using Advocu.WebApp.Extensions;

namespace Advocu.WebApp;

public class AdvocuDataService
{
    public List<AdvocuTag> GetTags()
    {
        return Enum.GetValues<AdvocuTag>()
            .OrderBy(t => t.GetDisplayName())
            .ToList();
    }
}
