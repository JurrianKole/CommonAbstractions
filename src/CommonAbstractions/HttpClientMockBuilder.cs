using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using CommonAbstractions.Interfaces;

namespace CommonAbstractions
{
    internal class DerivedHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage httpResponseMessage;

        public DerivedHttpMessageHandler()
        {
            this.httpResponseMessage = new HttpResponseMessage();
        }

        public void SetResponseContent(string content)
        {
            this.httpResponseMessage.Content = new StringContent(content);
        }

        public void SetResponseStatusCode(HttpStatusCode statusCode)
        {
            this.httpResponseMessage.StatusCode = statusCode;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.httpResponseMessage);
        }
    }

    public class HttpClientMockBuilder : IHttpClientMockBuilder
    {
        private string responseContent;

        private HttpStatusCode? statusCode;

        private HttpClient httpClient;

        private readonly DerivedHttpMessageHandler derivedHttpMessageHandler;

        public HttpClientMockBuilder()
        {
            this.derivedHttpMessageHandler = new DerivedHttpMessageHandler();
        }

        public HttpClient Build()
        {
            this.derivedHttpMessageHandler.SetResponseContent(this.responseContent ?? string.Empty);
            this.derivedHttpMessageHandler.SetResponseStatusCode(this.statusCode ?? HttpStatusCode.Accepted);

            this.httpClient = new HttpClient(this.derivedHttpMessageHandler);

            return this.httpClient;
        }

        public IHttpClientMockBuilder SetResponseContent(string responseContent)
        {
            this.responseContent = responseContent;

            return this;
        }

        public IHttpClientMockBuilder SetResponseStatusCode(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;

            return this;
        }
    }
}