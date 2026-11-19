using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Advocu.Core.Parsing;
using ModelContextProtocol.Server;

namespace Advocu.Mcp.Shared;

/// <summary>
/// Compact summary projection of an <see cref="ActivityItem"/> to keep MCP tool responses concise.
/// </summary>
public sealed class ActivitySummary
{
    [JsonPropertyName("activityId")]
    public required string ActivityId { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("activityDate")]
    public string? ActivityDate { get; init; }

    [JsonPropertyName("submissionDate")]
    public required string SubmissionDate { get; init; }

    [JsonPropertyName("updatedDate")]
    public required string UpdatedDate { get; init; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; init; }

    [JsonPropertyName("activityUrl")]
    public string? ActivityUrl { get; init; }

    [JsonPropertyName("private")]
    public bool? Private { get; init; }

    [JsonPropertyName("metrics")]
    public ActivityItemMetrics? Metrics { get; init; }

    public static ActivitySummary FromItem(ActivityItem item) => new()
    {
        ActivityId = item.ActivityId,
        Type = item.Type,
        Title = item.Data.Title,
        ActivityDate = item.Data.ActivityDate,
        SubmissionDate = item.SubmissionDate,
        UpdatedDate = item.UpdatedDate,
        Tags = item.Data.Tags,
        ActivityUrl = item.Data.ActivityUrl,
        Private = item.Data.Private,
        Metrics = item.Data.Metrics
    };
}

/// <summary>
/// Result returned by <see cref="AdvocuTools.SearchActivitiesAsync"/>.
/// </summary>
public sealed class ActivitySearchResult
{
    [JsonPropertyName("totalReturned")]
    public required int TotalReturned { get; init; }

    [JsonPropertyName("page")]
    public required int Page { get; init; }

    [JsonPropertyName("size")]
    public required int Size { get; init; }

    [JsonPropertyName("hasMore")]
    public required bool HasMore { get; init; }

    [JsonPropertyName("summaries")]
    public List<ActivitySummary>? Summaries { get; init; }

    [JsonPropertyName("activities")]
    public List<ActivityItem>? Activities { get; init; }
}

/// <summary>
/// Result returned by <see cref="AdvocuTools.DraftActivityAsync"/>.
/// </summary>
public sealed class DraftActivityResult
{
    [JsonPropertyName("dryRun")]
    public required bool DryRun { get; init; }

    [JsonPropertyName("valid")]
    public required bool Valid { get; init; }

    [JsonPropertyName("activityType")]
    public required string ActivityType { get; init; }

    [JsonPropertyName("activityId")]
    public string? ActivityId { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("validationErrors")]
    public List<string>? ValidationErrors { get; init; }

    [JsonPropertyName("validatedPayload")]
    public object? ValidatedPayload { get; init; }
}

/// <summary>
/// Result returned by <see cref="AdvocuTools.UpdateActivityAsync"/>.
/// </summary>
public sealed class UpdateActivityResult
{
    [JsonPropertyName("dryRun")]
    public required bool DryRun { get; init; }

    [JsonPropertyName("valid")]
    public required bool Valid { get; init; }

    [JsonPropertyName("activityId")]
    public required string ActivityId { get; init; }

    [JsonPropertyName("validationErrors")]
    public List<string>? ValidationErrors { get; init; }

    [JsonPropertyName("validatedPatch")]
    public PatchActivityRequest? ValidatedPatch { get; init; }

    [JsonPropertyName("updatedActivity")]
    public ActivityItem? UpdatedActivity { get; init; }
}

/// <summary>
/// Workflow-oriented MCP tools for managing Advocu GDE activities.
/// </summary>
[McpServerToolType]
public sealed class AdvocuTools(AdvocuApiClient apiClient)
{
    private readonly AdvocuApiClient _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

    /// <summary>
    /// Returns all Advocu API enumerations in a single JSON payload.
    /// </summary>
    [McpServerTool(Name = "get_enums", ReadOnly = true, Idempotent = true, Destructive = false, OpenWorld = false)]
    [Description("Get all Advocu API enumerations (tags, content types, event formats, interaction formats, interaction types, significance types, activity types, and countries) in one JSON response. Tool fallback for hosts that do not read the advocumcp://advocu/enums resource directly.")]
    public static AdvocuEnumsSnapshot GetEnums() => AdvocuEnumsSnapshot.Create();

    /// <summary>
    /// Searches and filters submitted Advocu activities.
    /// </summary>
    [McpServerTool(Name = "search_activities", ReadOnly = true, Idempotent = true, Destructive = false, OpenWorld = true)]
    [Description("Search and list submitted Advocu activities (GET /activities) with optional date range, type, tag, and keyword filters. Returns compact summaries by default (set summaryOnly=false for full payloads).")]
    public async Task<ActivitySearchResult> SearchActivitiesAsync(
        [Description("Optional inclusive lower bound for submission date (ISO-8601 UTC, e.g. 2026-01-01T00:00:00Z).")] DateTimeOffset? from = null,
        [Description("Optional exclusive upper bound for submission date (ISO-8601 UTC, e.g. 2026-12-31T23:59:59Z).")] DateTimeOffset? to = null,
        [Description("Optional lower bound for activities updated after this timestamp (ISO-8601 UTC).")] DateTimeOffset? updatedAfter = null,
        [Description("Optional activity type slug filter (e.g. public-speaking, workshop, content-creation).")] string? type = null,
        [Description("Optional tag filter (case-insensitive substring or exact tag name).")] string? tag = null,
        [Description("Optional keyword filter matched against title, description, and URL.")] string? keyword = null,
        [Description("Zero-based page number (default: 0).")] int page = 0,
        [Description("Page size from 1 to 100 (default: 20).")] int size = 20,
        [Description("When true (default), returns compact summaries to save context; when false, returns full ActivityItem payloads.")] bool summaryOnly = true,
        CancellationToken cancellationToken = default)
    {
        var clampedSize = Math.Clamp(size, 1, 100);
        var clampedPage = Math.Max(page, 0);

        var response = await _apiClient.GetActivitiesAsync(new GetActivitiesQuery
        {
            From = from,
            To = to,
            UpdatedAfter = updatedAfter,
            Page = clampedPage,
            Size = clampedSize
        }, cancellationToken);

        IEnumerable<ActivityItem> filtered = response.Content;

        if (!string.IsNullOrWhiteSpace(type))
        {
            var normalizedType = NormalizeActivityType(type);
            filtered = filtered.Where(a => string.Equals(a.Type, normalizedType, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(tag))
        {
            filtered = filtered.Where(a => a.Data.Tags?.Any(t => t.Contains(tag, StringComparison.OrdinalIgnoreCase)) == true);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            filtered = filtered.Where(a =>
                (a.Data.Title?.Contains(keyword, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (a.Data.DescriptionPlainText?.Contains(keyword, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (a.Data.Description?.Contains(keyword, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (a.Data.ActivityUrl?.Contains(keyword, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        var items = filtered.ToList();

        return new ActivitySearchResult
        {
            TotalReturned = items.Count,
            Page = clampedPage,
            Size = clampedSize,
            HasMore = response.Content.Count == clampedSize,
            Summaries = summaryOnly ? items.Select(ActivitySummary.FromItem).ToList() : null,
            Activities = summaryOnly ? null : items
        };
    }

    /// <summary>
    /// Unified tool for validating and drafting any supported Advocu activity type.
    /// </summary>
    [McpServerTool(Name = "draft_activity", ReadOnly = false, Idempotent = false, Destructive = false, OpenWorld = true)]
    [Description("Validate or create an Advocu activity draft for any of the 7 draftable types (content-creation, public-speaking, workshop, mentoring, product-feedback-given, interaction-with-googlers, stories). Supports fuzzy enum/tag matching and dryRun=true validation before posting.")]
    public async Task<DraftActivityResult> DraftActivityAsync(
        [Description("Activity type slug: content-creation, public-speaking, workshop, mentoring, product-feedback-given, interaction-with-googlers, or stories.")] string activityType,
        [Description("Activity title (3 to 200 characters).")] string title,
        [Description("Activity description (up to 2000 characters).")] string description,
        [Description("List of allowed tag names from advocumcp://advocu/enums.")] List<string>? tags = null,
        [Description("Activity date in YYYY-MM-DD format (required for all types except stories).")] string? activityDate = null,
        [Description("Activity HTTP/HTTPS URL (required for content-creation, public-speaking, workshop, mentoring, stories).")] string? activityUrl = null,
        [Description("Content type (required for content-creation and product-feedback-given).")] string? contentType = null,
        [Description("Event format: In-Person, Virtual, or Hybrid (required for public-speaking, workshop, mentoring).")] string? eventFormat = null,
        [Description("Country name (only for In-Person or Hybrid events).")] string? country = null,
        [Description("City name (optional, public-speaking).")] string? city = null,
        [Description("City Place ID (optional, public-speaking).")] string? cityPlaceId = null,
        [Description("Number of in-person attendees (only for Hybrid events).")] int? inPersonAttendees = null,
        [Description("Interaction format (required for interaction-with-googlers).")] string? format = null,
        [Description("Interaction type (required for interaction-with-googlers).")] string? interactionType = null,
        [Description("Product description, max 500 chars (optional, product-feedback-given).")] string? productDescription = null,
        [Description("Why the story is significant, max 2000 chars (required for stories).")] string? whyIsSignificant = null,
        [Description("Significance type (required for stories).")] string? significanceType = null,
        [Description("Reader count >= 1 (content-creation).")] int? readers = null,
        [Description("Attendee count >= 1 (public-speaking, workshop, mentoring).")] int? attendees = null,
        [Description("Time spent in minutes >= 1 (product-feedback-given, interaction-with-googlers).")] int? timeSpent = null,
        [Description("Impact count >= 1 (stories).")] int? impact = null,
        [Description("Optional additional info (max 2000 characters).")] string? additionalInfo = null,
        [Description("Optional additional links, max 2000 chars (interaction-with-googlers).")] string? additionalLinks = null,
        [Description("Whether the activity is private (optional).")] bool? isPrivate = null,
        [Description("When true, validates all fields and returns the resolved payload without posting to Advocu.")] bool dryRun = false,
        CancellationToken cancellationToken = default)
    {
        var normalizedType = NormalizeActivityType(activityType);
        List<string> errors = [];

        var parsedTags = ParseTagsWithErrors(tags, errors);

        ActivityRequest? request = normalizedType switch
        {
            "content-creation" => BuildContentCreation(title, description, parsedTags, activityDate, activityUrl, contentType, readers, additionalInfo, isPrivate, errors),
            "public-speaking" => BuildPublicSpeaking(title, description, parsedTags, activityDate, activityUrl, eventFormat, country, city, cityPlaceId, attendees, inPersonAttendees, additionalInfo, isPrivate, errors),
            "workshop" => BuildWorkshop(title, description, parsedTags, activityDate, activityUrl, eventFormat, country, attendees, inPersonAttendees, additionalInfo, isPrivate, errors),
            "mentoring" => BuildMentoring(title, description, parsedTags, activityDate, activityUrl, eventFormat, country, attendees, inPersonAttendees, additionalInfo, isPrivate, errors),
            "product-feedback-given" => BuildProductFeedback(title, description, parsedTags, activityDate, contentType, productDescription, timeSpent, additionalInfo, isPrivate, errors),
            "interaction-with-googlers" => BuildInteraction(title, description, parsedTags, activityDate, format, interactionType, timeSpent, additionalInfo, additionalLinks, isPrivate, errors),
            "stories" => BuildStories(title, description, parsedTags, activityUrl, whyIsSignificant, significanceType, impact, additionalInfo, isPrivate, errors),
            "github-repository" or "youtube-video" => AddError(errors, $"Activity type '{normalizedType}' is read-only and cannot be drafted."),
            _ => AddError(errors, $"Unsupported activityType '{activityType}'. Valid draftable types: content-creation, public-speaking, workshop, mentoring, product-feedback-given, interaction-with-googlers, stories.")
        };

        if (request is not null)
        {
            CollectValidationErrors(request, errors);
            CollectValidationErrors(request.Metrics, errors);
        }

        if (errors.Count > 0)
        {
            return new DraftActivityResult
            {
                DryRun = dryRun,
                Valid = false,
                ActivityType = normalizedType,
                ValidationErrors = errors
            };
        }

        if (dryRun)
        {
            return new DraftActivityResult
            {
                DryRun = true,
                Valid = true,
                ActivityType = normalizedType,
                ValidatedPayload = request
            };
        }

        var response = request switch
        {
            CreateContentCreationActivityRequest r => await _apiClient.PostContentCreationActivityAsync(r, cancellationToken),
            CreatePublicSpeakingActivityRequest r => await _apiClient.PostPublicSpeakingActivityAsync(r, cancellationToken),
            CreateWorkshopActivityRequest r => await _apiClient.PostWorkshopActivityAsync(r, cancellationToken),
            CreateMentoringActivityRequest r => await _apiClient.PostMentoringActivityAsync(r, cancellationToken),
            CreateProductFeedbackActivityRequest r => await _apiClient.PostProductFeedbackActivityAsync(r, cancellationToken),
            CreateInteractionWithGooglersActivityRequest r => await _apiClient.PostInteractionWithGooglersActivityAsync(r, cancellationToken),
            CreateStoriesActivityRequest r => await _apiClient.PostStoriesActivityAsync(r, cancellationToken),
            _ => throw new InvalidOperationException($"Unexpected request type for '{normalizedType}'.")
        };

        return new DraftActivityResult
        {
            DryRun = false,
            Valid = true,
            ActivityType = normalizedType,
            ActivityId = response.Id,
            Status = response.Status
        };
    }

    /// <summary>
    /// Partially updates an existing editable Advocu activity.
    /// </summary>
    [McpServerTool(Name = "update_activity", ReadOnly = false, Idempotent = true, Destructive = true, OpenWorld = true)]
    [Description("Partially update an existing editable Advocu activity by activityId (PATCH /activities/{activityId}). Only supplied non-null fields are updated. Supports fuzzy enum/tag matching and dryRun=true validation.")]
    public async Task<UpdateActivityResult> UpdateActivityAsync(
        [Description("The activityId of the activity to update.")] string activityId,
        [Description("Updated title (3 to 200 characters).")] string? title = null,
        [Description("Updated description (max 2000 characters).")] string? description = null,
        [Description("Updated tags from advocumcp://advocu/enums.")] List<string>? tags = null,
        [Description("Updated activity date in YYYY-MM-DD format.")] string? activityDate = null,
        [Description("Updated activity HTTP/HTTPS URL.")] string? activityUrl = null,
        [Description("Updated content type.")] string? contentType = null,
        [Description("Updated event format (In-Person, Virtual, Hybrid).")] string? eventFormat = null,
        [Description("Updated country.")] string? country = null,
        [Description("Updated in-person attendees (Hybrid events).")] int? inPersonAttendees = null,
        [Description("Updated interaction format.")] string? format = null,
        [Description("Updated interaction type.")] string? interactionType = null,
        [Description("Updated product description (max 500 chars).")] string? productDescription = null,
        [Description("Updated whyIsSignificant (max 2000 chars).")] string? whyIsSignificant = null,
        [Description("Updated significance type.")] string? significanceType = null,
        [Description("Updated readers metric (>= 1).")] int? readers = null,
        [Description("Updated attendees metric (>= 1).")] int? attendees = null,
        [Description("Updated timeSpent metric in minutes (>= 1).")] int? timeSpent = null,
        [Description("Updated impact metric (>= 1).")] int? impact = null,
        [Description("Updated additional info (max 2000 chars).")] string? additionalInfo = null,
        [Description("Updated additional links (max 2000 chars).")] string? additionalLinks = null,
        [Description("Updated privacy flag.")] bool? isPrivate = null,
        [Description("When true, validates the patch payload without calling Advocu.")] bool dryRun = false,
        CancellationToken cancellationToken = default)
    {
        List<string> errors = [];
        if (string.IsNullOrWhiteSpace(activityId))
        {
            errors.Add("activityId is required.");
        }

        List<string>? resolvedTags = null;
        if (tags is not null)
        {
            resolvedTags = ParseTagsWithErrors(tags, errors).Select(AdvocuEnumsSnapshot.GetApiValue).ToList();
        }

        ActivityMetrics? metrics = null;
        if (readers.HasValue || attendees.HasValue || timeSpent.HasValue || impact.HasValue)
        {
            metrics = new ActivityMetrics
            {
                Readers = readers,
                Attendees = attendees,
                TimesSpent = timeSpent,
                Impact = impact
            };
        }

        var patch = new PatchActivityRequest
        {
            Data = new PatchActivityData
            {
                Title = title,
                Description = description,
                Tags = resolvedTags,
                ActivityDate = activityDate,
                ActivityUrl = activityUrl,
                ContentType = ParseOptionalEnum<AdvocuActivityContentType>(contentType, nameof(contentType), errors),
                EventFormat = ParseOptionalEnum<AdvocuEventFormat>(eventFormat, nameof(eventFormat), errors),
                Country = ParseOptionalEnum<AdvocuCountry>(country, nameof(country), errors),
                InPersonAttendees = inPersonAttendees,
                Format = ParseOptionalEnum<AdvocuFormat>(format, nameof(format), errors),
                InteractionType = ParseOptionalEnum<AdvocuInteractionType>(interactionType, nameof(interactionType), errors),
                ProductDescription = productDescription,
                WhyIsSignificant = whyIsSignificant,
                SignificanceType = ParseOptionalEnum<AdvocuSignificanceType>(significanceType, nameof(significanceType), errors),
                Metrics = metrics,
                AdditionalInfo = additionalInfo,
                AdditionalLinks = additionalLinks,
                Private = isPrivate
            }
        };

        CollectValidationErrors(patch, errors);
        CollectValidationErrors(patch.Data, errors);
        if (patch.Data.Metrics is not null)
        {
            CollectValidationErrors(patch.Data.Metrics, errors);
        }

        if (errors.Count > 0)
        {
            return new UpdateActivityResult
            {
                DryRun = dryRun,
                Valid = false,
                ActivityId = activityId,
                ValidationErrors = errors
            };
        }

        if (dryRun)
        {
            return new UpdateActivityResult
            {
                DryRun = true,
                Valid = true,
                ActivityId = activityId,
                ValidatedPatch = patch
            };
        }

        var updated = await _apiClient.PatchActivityAsync(activityId, patch, cancellationToken);
        return new UpdateActivityResult
        {
            DryRun = false,
            Valid = true,
            ActivityId = updated.ActivityId,
            UpdatedActivity = updated
        };
    }

    private static string NormalizeActivityType(string value) =>
        value.Trim().ToLowerInvariant().Replace(' ', '-').Replace('_', '-');

    private static ActivityRequest? AddError(List<string> errors, string message)
    {
        errors.Add(message);
        return null;
    }

    private static List<AdvocuTag> ParseTagsWithErrors(List<string>? tags, List<string> errors)
    {
        List<AdvocuTag> parsed = [];
        if (tags is null)
        {
            return parsed;
        }

        foreach (var tag in tags.Where(t => !string.IsNullOrWhiteSpace(t)))
        {
            try
            {
                parsed.Add(AdvocuEnumParser.Parse<AdvocuTag>(tag));
            }
            catch (Exception ex)
            {
                errors.Add($"Invalid tag '{tag}': {ex.Message}");
            }
        }

        return [.. parsed.Distinct()];
    }

    private static T? ParseOptionalEnum<T>(string? value, string fieldName, List<string> errors) where T : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        try
        {
            return AdvocuEnumParser.Parse<T>(value);
        }
        catch (Exception ex)
        {
            errors.Add($"Invalid {fieldName} '{value}': {ex.Message}");
            return null;
        }
    }

    private static T ParseRequiredEnum<T>(string? value, string fieldName, List<string> errors) where T : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add($"{fieldName} is required.");
            return default;
        }

        return ParseOptionalEnum<T>(value, fieldName, errors) ?? default;
    }

    private static void CollectValidationErrors(object instance, List<string> errors)
    {
        var context = new ValidationContext(instance);
        List<ValidationResult> results = [];
        if (!Validator.TryValidateObject(instance, context, results, validateAllProperties: true))
        {
            foreach (var result in results)
            {
                if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                {
                    errors.Add(result.ErrorMessage);
                }
            }
        }
    }

    private static int ValidateRequiredPositiveMetric(int? value, string fieldName, List<string> errors)
    {
        if (!value.HasValue)
        {
            errors.Add($"{fieldName} is required and must be at least 1.");
            return 1;
        }

        if (value.Value < 1)
        {
            errors.Add($"{fieldName} must be at least 1 (received {value.Value}).");
            return 1;
        }

        return value.Value;
    }

    private static CreateContentCreationActivityRequest BuildContentCreation(
        string title, string description, List<AdvocuTag> tags, string? activityDate, string? activityUrl,
        string? contentType, int? readers, string? additionalInfo, bool? isPrivate, List<string> errors) => new()
    {
        Title = title,
        Description = description,
        Tags = tags,
        ContentType = ParseRequiredEnum<AdvocuActivityContentType>(contentType, nameof(contentType), errors),
        ActivityDate = activityDate ?? string.Empty,
        ActivityUrl = activityUrl ?? string.Empty,
        Readers = ValidateRequiredPositiveMetric(readers, nameof(readers), errors),
        AdditionalInfo = additionalInfo,
        Private = isPrivate
    };

    private static CreatePublicSpeakingActivityRequest BuildPublicSpeaking(
        string title, string description, List<AdvocuTag> tags, string? activityDate, string? activityUrl,
        string? eventFormat, string? country, string? city, string? cityPlaceId, int? attendees,
        int? inPersonAttendees, string? additionalInfo, bool? isPrivate, List<string> errors) => new()
    {
        Title = title,
        Description = description,
        Tags = tags,
        EventFormat = ParseRequiredEnum<AdvocuEventFormat>(eventFormat, nameof(eventFormat), errors),
        Country = ParseOptionalEnum<AdvocuCountry>(country, nameof(country), errors),
        City = string.IsNullOrWhiteSpace(city) ? null : city,
        CityPlaceId = string.IsNullOrWhiteSpace(cityPlaceId) ? null : cityPlaceId,
        Attendees = ValidateRequiredPositiveMetric(attendees, nameof(attendees), errors),
        InPersonAttendees = inPersonAttendees,
        ActivityDate = activityDate ?? string.Empty,
        ActivityUrl = activityUrl ?? string.Empty,
        AdditionalInfo = additionalInfo,
        Private = isPrivate
    };

    private static CreateWorkshopActivityRequest BuildWorkshop(
        string title, string description, List<AdvocuTag> tags, string? activityDate, string? activityUrl,
        string? eventFormat, string? country, int? attendees, int? inPersonAttendees,
        string? additionalInfo, bool? isPrivate, List<string> errors) => new()
    {
        Title = title,
        Description = description,
        Tags = tags,
        EventFormat = ParseRequiredEnum<AdvocuEventFormat>(eventFormat, nameof(eventFormat), errors),
        Country = ParseOptionalEnum<AdvocuCountry>(country, nameof(country), errors),
        Attendees = ValidateRequiredPositiveMetric(attendees, nameof(attendees), errors),
        InPersonAttendees = inPersonAttendees,
        ActivityDate = activityDate ?? string.Empty,
        ActivityUrl = activityUrl ?? string.Empty,
        AdditionalInfo = additionalInfo,
        Private = isPrivate
    };

    private static CreateMentoringActivityRequest BuildMentoring(
        string title, string description, List<AdvocuTag> tags, string? activityDate, string? activityUrl,
        string? eventFormat, string? country, int? attendees, int? inPersonAttendees,
        string? additionalInfo, bool? isPrivate, List<string> errors) => new()
    {
        Title = title,
        Description = description,
        Tags = tags,
        EventFormat = ParseRequiredEnum<AdvocuEventFormat>(eventFormat, nameof(eventFormat), errors),
        Country = ParseOptionalEnum<AdvocuCountry>(country, nameof(country), errors),
        Attendees = ValidateRequiredPositiveMetric(attendees, nameof(attendees), errors),
        InPersonAttendees = inPersonAttendees,
        ActivityDate = activityDate ?? string.Empty,
        ActivityUrl = activityUrl ?? string.Empty,
        AdditionalInfo = additionalInfo,
        Private = isPrivate
    };

    private static CreateProductFeedbackActivityRequest BuildProductFeedback(
        string title, string description, List<AdvocuTag> tags, string? activityDate, string? contentType,
        string? productDescription, int? timeSpent, string? additionalInfo, bool? isPrivate, List<string> errors) => new()
    {
        Title = title,
        Description = description,
        Tags = tags,
        ContentType = ParseRequiredEnum<AdvocuActivityContentType>(contentType, nameof(contentType), errors),
        ProductDescription = productDescription,
        TimeSpent = ValidateRequiredPositiveMetric(timeSpent, nameof(timeSpent), errors),
        ActivityDate = activityDate ?? string.Empty,
        AdditionalInfo = additionalInfo,
        Private = isPrivate
    };

    private static CreateInteractionWithGooglersActivityRequest BuildInteraction(
        string title, string description, List<AdvocuTag> tags, string? activityDate, string? format,
        string? interactionType, int? timeSpent, string? additionalInfo, string? additionalLinks, bool? isPrivate, List<string> errors) => new()
    {
        Title = title,
        Description = description,
        Tags = tags,
        Format = ParseRequiredEnum<AdvocuFormat>(format, nameof(format), errors),
        InteractionType = ParseRequiredEnum<AdvocuInteractionType>(interactionType, nameof(interactionType), errors),
        TimeSpent = ValidateRequiredPositiveMetric(timeSpent, nameof(timeSpent), errors),
        ActivityDate = activityDate ?? string.Empty,
        AdditionalInfo = additionalInfo,
        AdditionalLinks = additionalLinks,
        Private = isPrivate
    };

    private static CreateStoriesActivityRequest BuildStories(
        string title, string description, List<AdvocuTag> tags, string? activityUrl, string? whyIsSignificant,
        string? significanceType, int? impact, string? additionalInfo, bool? isPrivate, List<string> errors) => new()
    {
        Title = title,
        Description = description,
        Tags = tags,
        WhyIsSignificant = whyIsSignificant ?? string.Empty,
        SignificanceType = ParseRequiredEnum<AdvocuSignificanceType>(significanceType, nameof(significanceType), errors),
        ActivityUrl = activityUrl ?? string.Empty,
        Impact = ValidateRequiredPositiveMetric(impact, nameof(impact), errors),
        AdditionalInfo = additionalInfo,
        Private = isPrivate
    };
}
