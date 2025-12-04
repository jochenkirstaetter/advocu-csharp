using Advocu.WebApp.Components;
using Advocu;
using Advocu.WebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<AdvocuDataService>();

// Bind configuration to the settings model
builder.Services.Configure<AdvocuApiSettings>(builder.Configuration.GetSection("AdvocuApiSettings"));

// Register IHttpClientFactory
builder.Services.AddHttpClient();

// Register your Advocu Client
builder.Services.AddScoped<Advocu.AdvocuApiClient>(serviceProvider =>
{
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("AdvocuApi"); // Use a named client for specific configurations

    // Retrieve API settings
    var apiSettings = builder.Configuration.GetSection("AdvocuApiSettings").Get<AdvocuApiSettings>();
    if (apiSettings == null || string.IsNullOrEmpty(apiSettings.AccessToken))
    {
        throw new InvalidOperationException("Advocu API settings or AccessToken not configured.");
    }

    if (!string.IsNullOrEmpty(apiSettings.BaseUrl))
    {
        var uriBuilder = new UriBuilder(new Uri(apiSettings.BaseUrl));
        uriBuilder.Path = string.Concat(uriBuilder.Path.Replace("//", "/").TrimEnd('/'), '/');
        httpClient.BaseAddress = uriBuilder.Uri;
    }
    
    return new Advocu.AdvocuApiClient(httpClient, apiSettings.AccessToken);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
