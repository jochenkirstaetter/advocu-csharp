namespace Advocu.WebApp;

public class AdvocuDataService
{
    public List<AdvocuTag> GetTags()
    {
        return Enum.GetValues<AdvocuTag>().ToList();
    }
}