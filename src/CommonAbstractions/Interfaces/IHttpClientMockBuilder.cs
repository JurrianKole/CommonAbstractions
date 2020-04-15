using System.Net;
using System.Net.Http;

namespace CommonAbstractions.Interfaces
{
    public interface IHttpClientMockBuilder
    {
        HttpClient Build();

        IHttpClientMockBuilder SetResponseContent(string responseContent);

        IHttpClientMockBuilder SetResponseStatusCode(HttpStatusCode statusCode);
    }
}