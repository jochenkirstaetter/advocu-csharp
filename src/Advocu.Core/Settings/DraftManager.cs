using System.Text.Json;
using Advocu.NuGet.Commands;

namespace Advocu.NuGet.Settings;

/// <summary>
/// Manages the persistence of activity drafts.
/// </summary>
internal sealed class DraftManager
{
    private readonly string _draftFilePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="DraftManager"/> class.
    /// </summary>
    public DraftManager()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var advocuDir = Path.Combine(appData, "advocu");
        if (!Directory.Exists(advocuDir))
        {
            Directory.CreateDirectory(advocuDir);
        }
        _draftFilePath = Path.Combine(advocuDir, "drafts.json");
    }

    /// <summary>
    /// Saves the current draft to persistent storage.
    /// </summary>
    /// <param name="draft">The draft object to save.</param>
    public void SaveDraft(object draft)
    {
        var json = JsonSerializer.Serialize(draft, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_draftFilePath, json);
    }

    /// <summary>
    /// Loads a draft from persistent storage.
    /// </summary>
    /// <typeparam name="T">The type of the draft object.</typeparam>
    /// <returns>The loaded draft, or default if not found or invalid.</returns>
    public T? LoadDraft<T>()
    {
        if (!File.Exists(_draftFilePath))
        {
            return default;
        }

        try
        {
            var json = File.ReadAllText(_draftFilePath);
            return JsonSerializer.Deserialize<T>(json);
        }
        catch
        {
            return default;
        }
    }

    /// <summary>
    /// Clears the currently stored draft.
    /// </summary>
    public void ClearDraft()
    {
        if (File.Exists(_draftFilePath))
        {
            File.Delete(_draftFilePath);
        }
    }

    /// <summary>
    /// Checks if a draft exists in persistent storage.
    /// </summary>
    /// <returns>True if a draft exists, false otherwise.</returns>
    public bool HasDraft()
    {
        return File.Exists(_draftFilePath);
    }

    /// <summary>
    /// Gets the last modification time of the stored draft.
    /// </summary>
    /// <returns>The last write time of the draft file, or DateTime.MinValue if not found.</returns>
    public DateTime GetDraftLastModified()
    {
        return File.Exists(_draftFilePath) ? File.GetLastWriteTime(_draftFilePath) : DateTime.MinValue;
    }
}
