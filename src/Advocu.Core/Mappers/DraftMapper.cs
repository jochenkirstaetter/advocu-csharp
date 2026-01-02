using System.Reflection;
using System.Text.Json.Serialization;
using Advocu.NuGet.Models;

namespace Advocu.NuGet.Mappers;

/// <summary>
/// Maps ActivityDraft objects to specific ActivityRequest objects.
/// </summary>
internal sealed class DraftMapper
{
    public ActivityRequest Map(ActivityDraft draft)
    {
        return draft.ActivityType switch
        {
            "Content Creation" => new CreateContentCreationActivityRequest
            {
                Title = draft.Title!,
                Description = draft.Description!,
                ActivityDate = draft.ActivityDate!.Value.ToString("yyyy-MM-dd"),
                ActivityUrl = draft.ActivityUrl,
                AdditionalInfo = draft.AdditionalInfo,
                Private = draft.Private ?? false,
                Tags = ParseTags(draft.Tags),
                ContentType = ParseEnum<AdvocuActivityContentType>(draft.ContentType!),
                Readers = draft.Readers ?? 0
            },
            "Public Speaking" => new CreatePublicSpeakingActivityRequest
            {
                Title = draft.Title!,
                Description = draft.Description!,
                ActivityDate = draft.ActivityDate!.Value.ToString("yyyy-MM-dd"),
                ActivityUrl = draft.ActivityUrl!,
                AdditionalInfo = draft.AdditionalInfo,
                Private = draft.Private ?? false,
                Tags = ParseTags(draft.Tags),
                Country = ParseEnum<AdvocuCountry>(draft.Country!),
                City = draft.City,
                EventFormat = ParseEnum<AdvocuEventFormat>(draft.EventFormat!),
                Attendees = draft.Attendees ?? 0
            },
            "Workshop" => new CreateWorkshopActivityRequest
            {
                Title = draft.Title!,
                Description = draft.Description!,
                ActivityDate = draft.ActivityDate!.Value.ToString("yyyy-MM-dd"),
                ActivityUrl = draft.ActivityUrl!,
                AdditionalInfo = draft.AdditionalInfo,
                Private = draft.Private ?? false,
                Tags = ParseTags(draft.Tags),
                Country = ParseEnum<AdvocuCountry>(draft.Country!),
                EventFormat = ParseEnum<AdvocuEventFormat>(draft.EventFormat!),
                Attendees = draft.Attendees ?? 0
            },
            "Mentoring" => new CreateMentoringActivityRequest
            {
                Title = draft.Title!,
                Description = draft.Description!,
                ActivityDate = draft.ActivityDate!.Value.ToString("yyyy-MM-dd"),
                ActivityUrl = draft.ActivityUrl!,
                AdditionalInfo = draft.AdditionalInfo,
                Private = draft.Private ?? false,
                Tags = ParseTags(draft.Tags),
                Attendees = draft.Mentees ?? 0,
                EventFormat = ParseEnum<AdvocuEventFormat>(draft.EventFormat!),
                Country = draft.EventFormat == "Virtual" ? null : ParseEnum<AdvocuCountry>(draft.Country!)
            },
            "Product Feedback" => new CreateProductFeedbackActivityRequest
            {
                Title = draft.Title!,
                Description = draft.Description!,
                ActivityDate = draft.ActivityDate!.Value.ToString("yyyy-MM-dd"),
                ProductDescription = draft.ProductTeam!,
                AdditionalInfo = draft.AdditionalInfo,
                Private = draft.Private ?? false,
                Tags = ParseTags(draft.Tags),
                ContentType = ParseEnum<AdvocuActivityContentType>(draft.ContentType!),
                TimeSpent = draft.Hours ?? 0 
            },
            "Interaction" => new CreateInteractionWithGooglersActivityRequest
            {
                Title = draft.Title!,
                Description = draft.Description!,
                ActivityDate = draft.ActivityDate!.Value.ToString("yyyy-MM-dd"),
                AdditionalLinks = draft.ActivityUrl ?? "",
                AdditionalInfo = draft.AdditionalInfo,
                Private = draft.Private ?? false,
                Tags = ParseTags(draft.Tags),
                Format = ParseEnum<AdvocuFormat>(draft.EventFormat!),
                InteractionType = ParseEnum<AdvocuInteractionType>(draft.InteractionType!),
                TimeSpent = draft.Hours ?? 0
            },
            "Stories" => new CreateStoriesActivityRequest
            {
                Title = draft.Title!,
                Description = draft.Description!,
                ActivityUrl = draft.ActivityUrl!,
                AdditionalInfo = draft.AdditionalInfo,
                Private = draft.Private ?? false,
                Tags = ParseTags(draft.Tags),
                SignificanceType = ParseEnum<AdvocuSignificanceType>(draft.StoryType!),
                WhyIsSignificant = draft.Significance!,
                Impact = draft.Attendees 
            },
            _ => throw new NotImplementedException($"Submission for {draft.ActivityType} not implemented yet.")
        };
    }

    private List<AdvocuTag> ParseTags(List<string> tags)
    {
        return tags.Select(ParseEnum<AdvocuTag>).ToList();
    }

    private T ParseEnum<T>(string displayName) where T : struct, Enum
    {
        // Reverse lookup
        foreach (var value in Enum.GetValues<T>())
        {
            if (GetEnumDisplayName(value) == displayName) return value;
        }
        return Enum.Parse<T>(displayName); // Fallback
    }

    private string GetEnumDisplayName<T>(T value) where T : struct, Enum
    {
        var memberInfo = typeof(T).GetMember(value.ToString()).FirstOrDefault();
        if (memberInfo != null)
        {
            var attr = memberInfo.GetCustomAttribute<JsonStringEnumMemberNameAttribute>();
            if (attr != null) return attr.Name;
        }
        return value.ToString();
    }
}
