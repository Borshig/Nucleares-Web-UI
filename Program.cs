using MudBlazor.Services;
using NuclearesWebUI.Components;




var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();



namespace NuclearesGETData
{
    public class Programs
    {
        static HttpClient httpClient = new HttpClient();

        public static Dictionary<string, string> Second()
        {
            var rawRoot = httpClient.GetFromJsonAsync<Root>("http://host.docker.internal:8785/?variable=WEBSERVER_LIST_VARIABLES_JSON").Result;

            var result = new Dictionary<string, string>();

            if (rawRoot != null)
            {
                string[] exclude = { "JSON", "VARIABLES", "GET", "HTML" };

                foreach (var variable in rawRoot.GET)
                {
                    string suffix = variable.Substring(variable.LastIndexOf("_") + 1);
                    if (!exclude.Contains(suffix))
                    {
                        string variableData = httpClient.GetStringAsync($"http://host.docker.internal:8785/?variable={variable}").Result;
                        result[variable] = variableData;
                    }
                }
            }
            //var heatMapSeries = new List<ChartSeries<double>>();
            //heatMapSeries.Add(new ChartSeries<double> { TooltipYValueFormat = "2" });
            return result;

        }
        public class Root
        {
            public List<string> GET { get; set; } = new List<string>();
        }
    }
}