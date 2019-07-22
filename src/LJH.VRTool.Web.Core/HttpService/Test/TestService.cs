using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LJH.VRTool.HttpService.Test
{
    public class TestService
    {
        public HttpClient Client { get; }

        public TestService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.github.com/");
            // GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept",
                "application/vnd.github.v3+json");
            // GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent",
                "HttpClientFactory-Sample");

            Client = client;
        }
        public async Task<IEnumerable<string>> GetAspNetDocsIssues()
        {
            var response = await Client.GetAsync(
                "/repos/aspnet/docs/issues?state=open&sort=created&direction=desc");
            response.EnsureSuccessStatusCode();
            var result = await response.Content
                .ReadAsAsync<IEnumerable<string>>();
            return result;
        }
    }
}
