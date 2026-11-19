using System.Text.Json.Serialization;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Advocu;

/// <summary>
/// Specifies the tags associated with an activity.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AdvocuTag>))]
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
    [JsonStringEnumMemberName("Cloud - BigQuery")]
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
    [JsonStringEnumMemberName("Cloud - Conversational Data Analytics")]
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
