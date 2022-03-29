namespace API_Gateway.Services
{
    public interface IHttpClientWrapper
    {
        Task<string> CallApiWithParams(string apiUrl, string param, string paramName);
    }
}
