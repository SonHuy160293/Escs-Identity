using Microsoft.Extensions.Configuration;

namespace Core.Infrastructure.DependencyModels
{
    public class DependencyOptions
    {
        // DbContext Handler
        public bool EnableDbContextHandler { get; set; }


        // Http Client Factory
        public bool EnableHttpClient { get; set; }



        // Authentication
        public bool EnableAuthentication { get; set; }
        public IConfiguration? TokenOptions { get; set; }

    }
}
