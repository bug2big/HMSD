using API_Gateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiGatewayController : ControllerBase
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        private const string HttpMethodEncryptionName = "Encrypt";
        private const string HttpMethodDecryptionName = "Decrypt";

        public ApiGatewayController(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        [HttpGet(Name = HttpMethodEncryptionName)]
        public async Task<string> Encrypt(string plainText)
        {
            return await _httpClientWrapper.CallApiWithParams(HttpMethodEncryptionName, plainText, "plainText");  
        }

        [HttpGet(Name = HttpMethodDecryptionName)]
        public async Task<string> Decrypt(string plainText)
        {
            return await _httpClientWrapper.CallApiWithParams(HttpMethodDecryptionName, plainText, "plainText");
        }
    }
}