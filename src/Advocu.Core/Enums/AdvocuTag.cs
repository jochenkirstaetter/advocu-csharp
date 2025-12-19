using System.Text.Json.Serialization;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Advocu;
// 
// // Note: The 'Tags' enum will be hard to maintain as an enum due to its size and dynamic nature.
// Consider using a string array or a dedicated class for handling tags.  If you still need an enum:
/// <summary>
/// Specifies the tags associated with an activity.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AdvocuTag>))]
public enum AdvocuTag
{
    [JsonStringEnumMemberName("AI")]
    Ai,
    [JsonStringEnumMemberName("AI - AI Studio")]
    AiAiStudio,
    [JsonStringEnumMemberName("AI - Agent Development Kit (ADK)")]
    AiAgentDevelopmentKitAdk,
    [JsonStringEnumMemberName("AI - Agents")]
    AiAgents,
    [JsonStringEnumMemberName("AI - Colab")]
    AiColab,
    [JsonStringEnumMemberName("AI - Gemini")]
    AiGemini,
    [JsonStringEnumMemberName("AI - Gemma")]
    AiGemma,
    [JsonStringEnumMemberName("AI - Generative AI")]
    AiGenerativeAi,
    [JsonStringEnumMemberName("AI - JAX")]
    AiJax,
    [JsonStringEnumMemberName("AI - Kaggle")]
    AiKaggle,
    [JsonStringEnumMemberName("AI - Keras")]
    AiKeras,
    [JsonStringEnumMemberName("AI - LLM")]
    AiLlm,
    [JsonStringEnumMemberName("AI - LiteRT")]
    AiLiteRt,
    [JsonStringEnumMemberName("AI - ML Engineering (MLOps)")]
    AiMlEngineeringMlOps,
    [JsonStringEnumMemberName("AI - MediaPipe")]
    AiMediaPipe,
    [JsonStringEnumMemberName("AI - On Device AI")]
    AiOnDeviceAi,
    [JsonStringEnumMemberName("AI - Responsible AI")]
    AiResponsibleAi,
    [JsonStringEnumMemberName("AI - TPU")]
    AiTpu,
    [JsonStringEnumMemberName("AI - TensorFlow")]
    AiTensorFlow,
    [JsonStringEnumMemberName("AI - Vertex AI")]
    AiVertexAi,
    [JsonStringEnumMemberName("AI Math Clubs")]
    AiMathClubs,
    [JsonStringEnumMemberName("AI Paper Reading Clubs")]
    AiPaperReadingClubs,
    [JsonStringEnumMemberName("AR/VR")]
    AiArVr,
    [JsonStringEnumMemberName("Android")]
    Android,
    [JsonStringEnumMemberName("Android - ARCore")]
    AndroidArCore,
    [JsonStringEnumMemberName("Android - Android Auto")]
    AndroidAndroidAuto,
    [JsonStringEnumMemberName("Android - Android Dev Tools")]
    AndroidAndroidDevTools,
    [JsonStringEnumMemberName("Android - Android Studio")]
    AndroidAndroidStudio,
    [JsonStringEnumMemberName("Android - Android TV")]
    AndroidAndroidTv,
    [JsonStringEnumMemberName("Android - App Architecture & Arch Components")]
    AndroidAppArchitectureArchComponents,
    [JsonStringEnumMemberName("Android - App Indexing")]
    AndroidAppIndexing,
    [JsonStringEnumMemberName("Android - App Performance")]
    AndroidAppPerformance,
    [JsonStringEnumMemberName("Android - App Quality")]
    AndroidAppQuality,
    [JsonStringEnumMemberName("Android - Camera")]
    AndroidCamera,
    [JsonStringEnumMemberName("Android - Composer")]
    AndroidComposer,
    [JsonStringEnumMemberName("Android - Form Factors")]
    AndroidFormFactors,
    [JsonStringEnumMemberName("Android - Gemini Nano")]
    AndroidGeminiNano,
    [JsonStringEnumMemberName("Android - Jetpack")]
    AndroidJetpack,
    [JsonStringEnumMemberName("Android - Jetpack Compose")]
    AndroidJetpackCompose,
    [JsonStringEnumMemberName("Android - Kotlin")]
    AndroidKotlin,
    [JsonStringEnumMemberName("Android - Material Design")]
    AndroidMaterialDesign,
    [JsonStringEnumMemberName("Android - Media")]
    AndroidMedia,
    [JsonStringEnumMemberName("Android - Modern Android Development")]
    AndroidModernAndroidDevelopment,
    [JsonStringEnumMemberName("Android - Modern UI")]
    AndroidModernUi,
    [JsonStringEnumMemberName("Android - NDK")]
    AndroidNdk,
    [JsonStringEnumMemberName("Android - Passkeys")]
    AndroidPasskeys,
    [JsonStringEnumMemberName("Android - Performance")]
    AndroidPerformance,
    [JsonStringEnumMemberName("Android - Vulkan")]
    AndroidVulkan,
    [JsonStringEnumMemberName("Android - Wear OS")]
    AndroidWearOs,
    [JsonStringEnumMemberName("Android - Widgets")]
    AndroidWidgets,
    [JsonStringEnumMemberName("Android - XR")]
    AndroidXr,
    [JsonStringEnumMemberName("Angular")]
    Angular,
    [JsonStringEnumMemberName("Build with AI")]
    BuildWithAi,
    [JsonStringEnumMemberName("Cloud - AI Tools")]
    CloudAiTools,
    [JsonStringEnumMemberName("Cloud - API Gateways")]
    CloudAiGateways,
    [JsonStringEnumMemberName("Cloud - App Development")]
    CloudAppDevelopment,
    [JsonStringEnumMemberName("Cloud - Compute, Networking, Storage")]
    CloudComputeNetworkingStorage,
    [JsonStringEnumMemberName("Cloud - Data")]
    CloudData,
    [JsonStringEnumMemberName("Cloud - Operations & Management")]
    CloudOperationsManagement,
    [JsonStringEnumMemberName("Cloud - Security")]
    CloudSecurity,
    [JsonStringEnumMemberName("Cloud - Serverless & Containers")]
    CloudServerlessContainers,
    [JsonStringEnumMemberName("Cloud Study Jam")]
    CloudStudyJam,
    [JsonStringEnumMemberName("Dart - Flutter")]
    Dart,
    [JsonStringEnumMemberName("DevFest")]
    DevFest,
    [JsonStringEnumMemberName("Diversity & Inclusion")]
    DiversityInclusion,
    [JsonStringEnumMemberName("Earth Engine")]
    EarthEngine,
    [JsonStringEnumMemberName("Firebase")]
    Firebase,
    [JsonStringEnumMemberName("Firebase - A/B Testing")]
    FirebaseABTesting,
    [JsonStringEnumMemberName("Firebase - AI Logic")]
    FirebaseAiLogic,
    [JsonStringEnumMemberName("Firebase - AI Monitoring")]
    FirebaseAiMonitoring,
    [JsonStringEnumMemberName("Firebase - Analytics")]
    FirebaseAnalytics,
    [JsonStringEnumMemberName("Firebase - App Hosting")]
    FirebaseAppHosting,
    [JsonStringEnumMemberName("Firebase - Authentication")]
    FirebaseAuthentication,
    [JsonStringEnumMemberName("Firebase - Cloud Messaging")]
    FirebaseCloudMessaging,
    [JsonStringEnumMemberName("Firebase - Data Connect")]
    FirebaseDataConnect,
    [JsonStringEnumMemberName("Firebase - Firebase Studio")]
    FirebaseFirebaseStudio,
    [JsonStringEnumMemberName("Firebase - Firestore")]
    FirebaseFirestore,
    [JsonStringEnumMemberName("Firebase - Genkit")]
    FirebaseGenkit,
    [JsonStringEnumMemberName("Firebase - Performance")]
    FirebasePerformance,
    [JsonStringEnumMemberName("Firebase - Realtime Database")]
    FirebaseRealtimeDatabase,
    [JsonStringEnumMemberName("Firebase - Remote Config")]
    FirebaseRemoteConfig,
    [JsonStringEnumMemberName("Firebase - Test Lab")]
    FirebaseTestLab,
    [JsonStringEnumMemberName("Flutter")]
    Flutter,
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
    [JsonStringEnumMemberName("International Women's Day")]
    InternationalWomenSDay,
    [JsonStringEnumMemberName("ML Study Jams")]
    MlStudyJams,
    [JsonStringEnumMemberName("Open Source")]
    OpenSource,
    [JsonStringEnumMemberName("Payments")]
    Payments,
    [JsonStringEnumMemberName("Payments - Google Pay")]
    PaymentsGooglePay,
    [JsonStringEnumMemberName("Payments - Google Wallet")]
    PaymentsGoogleWallet,
    [JsonStringEnumMemberName("Road to Google Developers Certification")]
    RoadToGoogleDevelopersCertification,
    [JsonStringEnumMemberName("UX / UI Design")]
    UxUiDesign,
    [JsonStringEnumMemberName("Web - AI")]
    WebAi,
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
