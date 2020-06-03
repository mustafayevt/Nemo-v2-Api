using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Nemo_v2_Api;
using Xunit;

namespace ApiTest.ControllerTests
{
    public class BuyerControllerTest
    {
        private readonly HttpClient _httpClient;

        public BuyerControllerTest()
        {
            var webHostBuilder =
                new WebHostBuilder()
                    .UseEnvironment("Development") 
                    .UseStartup<Startup>();

            var server = new TestServer(webHostBuilder);
            _httpClient = server.CreateClient();
        }

        [Theory]
        [InlineData(-1)]
        public async Task GetBuyerById(long id)
        {
            
            //Arrange
            var request  = new HttpRequestMessage(HttpMethod.Get,$"/api/Buyer/GetBuyer/{id}");
            
            //Act
            var response = await _httpClient.SendAsync(request);
            
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
        }
    }
}