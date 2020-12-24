using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimonsSearch.Client.Clients;

namespace SimonsSearch.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services
                .AddBlazorise( options =>
                {
                    options.ChangeTextOnKeyPress = false;
                } )
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri("https://localhost:5011")});

            builder.Services.AddHttpClient<SearchClient>(client => client.BaseAddress = new Uri("https://localhost:5011"));

            await builder.Build().RunAsync();
        }
    }
}