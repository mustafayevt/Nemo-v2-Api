using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Nemo_v2_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("https://localhost:4041","http://localhost:4040");
    }
}
