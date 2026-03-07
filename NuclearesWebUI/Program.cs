using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using NuclearesWebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

namespace NuclearesGETData
{
    public class Programs
    {
        static HttpClient httpClient = new HttpClient();

        public static async Task<Dictionary<string, string>> Second()
        {
            var rawRoot = await httpClient.GetFromJsonAsync<Root>("http://localhost:8785/?variable=WEBSERVER_LIST_VARIABLES_JSON");

            var result = new Dictionary<string, string>();

            if (rawRoot?.GET != null)
            {
                string[] exclude = { "JSON", "VARIABLES", "GET", "HTML" };

                foreach (var variable in rawRoot.GET)
                {
                    string suffix = variable.Substring(variable.LastIndexOf("_") + 1);
                    if (!exclude.Contains(suffix))
                    {
                        string variableData = await httpClient.GetStringAsync($"http://localhost:8785/?variable={variable}");
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
