using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace TodoApp.Api.IntegrationTests
{
    public abstract class TestBase
    {
        protected readonly WebApplicationFactory<Startup> factory;
        protected readonly HttpClient httpClient;
        public abstract string EndpointRoute { get; }

        public TestBase()
        {
            factory = new TestWebApplicationFactory();
            httpClient = factory.CreateClient();
        }
    }
}
