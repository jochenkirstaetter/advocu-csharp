using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Advocu;

/// <summary>
/// Specifies the tags associated with an activity.
/// </summary>
[JsonConverter(typeof(AdvocuTagJsonConverter))]
public enum AdvocuTag
{
    [JsonStringEnumMemberName("AI")]
    Ai,
    [JsonStringEnumMemberName("AI - A2UI")]
    AiA2Ui,
    [JsonStringEnumMemberName("AI - AI Studio")]
    AiAiStudio,
    [JsonStringEnumMemberName("AI - Agent Development Kit (ADK)")]
    AiAgentDevelopmentKitAdk,
    [JsonStringEnumMemberName("AI - Antigravity")]
    AiAntigravity,
    [JsonStringEnumMemberName("AI - Antigravity CLI")]
    AiAntigravityCli,
    [JsonStringEnumMemberName("AI - Colab")]
    AiColab,
    [JsonStringEnumMemberName("AI - Gemini")]
    AiGemini,
    [JsonStringEnumMemberName("AI - Gemma")]
    AiGemma,
    [JsonStringEnumMemberName("AI - Genkit")]
    AiGenkit,
    [JsonStringEnumMemberName("AI - JAX")]
    AiJax,
    [JsonStringEnumMemberName("AI - Kaggle")]
    AiKaggle,
    [JsonStringEnumMemberName("AI - Keras")]
    AiKeras,
    [JsonStringEnumMemberName("AI - LiteRT")]
    AiLiteRt,
    [JsonStringEnumMemberName("AI - MediaPipe")]
    AiMediaPipe,
    [JsonStringEnumMemberName("AI - TPU")]
    AiTpu,
    [JsonStringEnumMemberName("AI - TorchTPU")]
    AiTorchTpu,
    [JsonStringEnumMemberName("AI - vLLM")]
    AiVLlm,
    [JsonStringEnumMemberName("Android")]
    Android,
    [JsonStringEnumMemberName("Android - Adaptive")]
    AndroidAdaptive,
    [JsonStringEnumMemberName("Android - Developer Productivity")]
    AndroidDeveloperProductivity,
    [JsonStringEnumMemberName("Android - Differentiated")]
    AndroidDifferentiated,
    [JsonStringEnumMemberName("Android - Intelligent")]
    AndroidIntelligent,
    [JsonStringEnumMemberName("Android - XR")]
    AndroidXr,
    [JsonStringEnumMemberName("Angular")]
    Angular,
    [JsonStringEnumMemberName("Build with AI")]
    BuildWithAi,
    [JsonStringEnumMemberName("Cloud - Agent Sandbox")]
    CloudAgentSandbox,
    [JsonStringEnumMemberName("Cloud - Agent Substrate")]
    CloudAgentSubstrate,
    [JsonStringEnumMemberName("Cloud - AlloyDB")]
    CloudAlloyDb,
    // Advocu's Personal API schema requires a literal trailing space on this tag value.
    [JsonStringEnumMemberName("Cloud - BigQuery ")]
    CloudBigQuery,
    [JsonStringEnumMemberName("Cloud - Cloud Developer Agent Plugin")]
    CloudCloudDeveloperAgentPlugin,
    [JsonStringEnumMemberName("Cloud - Cloud Firestore")]
    CloudCloudFirestore,
    [JsonStringEnumMemberName("Cloud - Cloud Run")]
    CloudCloudRun,
    [JsonStringEnumMemberName("Cloud - Cloud SQL")]
    CloudCloudSql,
    [JsonStringEnumMemberName("Cloud - CodeMender")]
    CloudCodeMender,
    // Advocu's Personal API schema requires a literal trailing space on this tag value.
    [JsonStringEnumMemberName("Cloud - Conversational Data Analytics ")]
    CloudConversationalDataAnalytics,
    [JsonStringEnumMemberName("Cloud - Data Agent Kit")]
    CloudDataAgentKit,
    [JsonStringEnumMemberName("Cloud - GKE")]
    CloudGke,
    [JsonStringEnumMemberName("Cloud - Gemini Platform")]
    CloudGeminiPlatform,
    [JsonStringEnumMemberName("Cloud - Lakehouse for Apache Iceberg")]
    CloudLakehouseForApacheIceberg,
    [JsonStringEnumMemberName("Cloud - Model Armor")]
    CloudModelArmor,
    [JsonStringEnumMemberName("Cloud - OneMCP")]
    CloudOneMcp,
    [JsonStringEnumMemberName("Dart / Flutter")]
    DartFlutter,
    [JsonStringEnumMemberName("DevFest")]
    DevFest,
    [JsonStringEnumMemberName("Earth Engine")]
    EarthEngine,
    [JsonStringEnumMemberName("Firebase")]
    Firebase,
    [JsonStringEnumMemberName("Firebase - AI Logic")]
    FirebaseAiLogic,
    [JsonStringEnumMemberName("Firebase - AI Monitoring")]
    FirebaseAiMonitoring,
    [JsonStringEnumMemberName("Firebase - App Check")]
    FirebaseAppCheck,
    [JsonStringEnumMemberName("Firebase - App Distribution")]
    FirebaseAppDistribution,
    [JsonStringEnumMemberName("Firebase - App Hosting")]
    FirebaseAppHosting,
    [JsonStringEnumMemberName("Firebase - Authentication")]
    FirebaseAuthentication,
    [JsonStringEnumMemberName("Firebase - Cloud Messaging")]
    FirebaseCloudMessaging,
    [JsonStringEnumMemberName("Firebase - Crashlytics")]
    FirebaseCrashlytics,
    [JsonStringEnumMemberName("Firebase - Firestore")]
    FirebaseFirestore,
    [JsonStringEnumMemberName("Firebase - Functions")]
    FirebaseFunctions,
    [JsonStringEnumMemberName("Firebase - Performance")]
    FirebasePerformance,
    [JsonStringEnumMemberName("Firebase - Realtime Database")]
    FirebaseRealtimeDatabase,
    [JsonStringEnumMemberName("Firebase - Remote Config")]
    FirebaseRemoteConfig,
    [JsonStringEnumMemberName("Firebase - SQL Connect")]
    FirebaseSqlConnect,
    [JsonStringEnumMemberName("Flutter - Gen UI")]
    FlutterGenUi,
    [JsonStringEnumMemberName("Golang")]
    Golang,
    [JsonStringEnumMemberName("Google Cloud")]
    GoogleCloud,
    [JsonStringEnumMemberName("Google I/O Extended")]
    GoogleIOExtended,
    [JsonStringEnumMemberName("Google Maps Platform")]
    GoogleMapsPlatform,
    [JsonStringEnumMemberName("Google Workspace")]
    GoogleWorkspace,
    [JsonStringEnumMemberName("Identity")]
    Identity,
    [JsonStringEnumMemberName("Identity - Google OAuth")]
    IdentityGoogleOAuth,
    [JsonStringEnumMemberName("Identity - Sign in with Google")]
    IdentitySignInWithGoogle,
    [JsonStringEnumMemberName("Open Source")]
    OpenSource,
    [JsonStringEnumMemberName("Payments")]
    Payments,
    [JsonStringEnumMemberName("Payments - Google Pay")]
    PaymentsGooglePay,
    [JsonStringEnumMemberName("Payments - Google Wallet")]
    PaymentsGoogleWallet,
    [JsonStringEnumMemberName("UX / UI Design")]
    UxUiDesign,
    [JsonStringEnumMemberName("Web")]
    Web,
    [JsonStringEnumMemberName("Web - AI for Web Developers")]
    WebAiForWebDevelopers,
    [JsonStringEnumMemberName("Web - Browser Extensions")]
    WebBrowserExtensions,
    [JsonStringEnumMemberName("Web - CSS & UI")]
    WebCssUi,
    [JsonStringEnumMemberName("Web - DevTools & Browser Automation")]
    WebDevToolsBrowserAutomation,
    [JsonStringEnumMemberName("Web - Fugu/PWA APIs")]
    WebFuguPwAsApIs,
    [JsonStringEnumMemberName("Web - Identity")]
    WebIdentity,
    [JsonStringEnumMemberName("Web - Performance")]
    WebPerformance,
    [JsonStringEnumMemberName("Workspace - Add Ons")]
    WorkspaceAddOns,
    [JsonStringEnumMemberName("Workspace - AppSheet")]
    WorkspaceAppSheet,
    [JsonStringEnumMemberName("Workspace - Google Apps Script")]
    WorkspaceGoogleAppsScript,
    [JsonStringEnumMemberName("Workspace - Google Workspace (REST) APIs")]
    WorkspaceGoogleWorkspaceRestApIs
}

/// <summary>
/// Custom JSON converter for <see cref="AdvocuTag"/> that preserves exact upstream wire values
/// (including trailing whitespace required by Advocu's Personal API schema on certain tags)
/// while still accepting trimmed or enum-name values on deserialization.
/// </summary>
public sealed class AdvocuTagJsonConverter : JsonConverter<AdvocuTag>
{
    private static readonly Dictionary<AdvocuTag, string> ToWire = BuildToWire();
    private static readonly Dictionary<string, AdvocuTag> FromWire = BuildFromWire();

    private static Dictionary<AdvocuTag, string> BuildToWire()
    {
        var map = new Dictionary<AdvocuTag, string>();
        foreach (var field in typeof(AdvocuTag).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var value = (AdvocuTag)field.GetValue(null)!;
            var wire = field.GetCustomAttribute<JsonStringEnumMemberNameAttribute>()?.Name ?? field.Name;
            map[value] = wire;
        }
        return map;
    }

    private static Dictionary<string, AdvocuTag> BuildFromWire()
    {
        var map = new Dictionary<string, AdvocuTag>(StringComparer.OrdinalIgnoreCase);
        foreach (var field in typeof(AdvocuTag).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var value = (AdvocuTag)field.GetValue(null)!;
            map[field.Name] = value;
            var wire = field.GetCustomAttribute<JsonStringEnumMemberNameAttribute>()?.Name;
            if (!string.IsNullOrEmpty(wire))
            {
                map[wire] = value;
                map[wire.Trim()] = value;
            }
        }
        return map;
    }

    public override AdvocuTag Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string token for {nameof(AdvocuTag)}.");
        }

        var raw = reader.GetString();
        if (raw is not null && (FromWire.TryGetValue(raw, out var tag) || FromWire.TryGetValue(raw.Trim(), out tag)))
        {
            return tag;
        }

        throw new JsonException($"Unknown {nameof(AdvocuTag)} value: '{raw}'.");
    }

    public override void Write(Utf8JsonWriter writer, AdvocuTag value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(ToWire.TryGetValue(value, out var wire) ? wire : value.ToString());
    }
}
