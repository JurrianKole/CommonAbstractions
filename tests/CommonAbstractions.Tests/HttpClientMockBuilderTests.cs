using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace CommonAbstractions.Tests
{
    public class HttpClientMockBuilderTests
    {
        [Fact]
        public async Task Build_NoPropertiesSet_ContentIsEmptyStringAndStatusCodeIsAccepted()
        {
            // Arrange
            var httpClientMockBuilder = new HttpClientMockBuilder();
            var httpClient = httpClientMockBuilder.Build();

            // Act
            var asyncResult = await httpClient.GetAsync("https://foo.bar.com/").ConfigureAwait(false);
            var contentResult = await asyncResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            var statusCode = asyncResult.StatusCode;

            // Assert
            Assert.Equal(string.Empty, contentResult);
            Assert.Equal(HttpStatusCode.Accepted, statusCode);
        }

        [Fact]
        public async Task SetResponseContent_Null_ContentIsEmptyString()
        {
            // Arrange
            var httpClientMockBuilder = new HttpClientMockBuilder();
            httpClientMockBuilder.SetResponseContent(null);
            var httpClient = httpClientMockBuilder.Build();

            // Act
            var asyncResult = await httpClient.GetAsync("https://foo.bar.com/").ConfigureAwait(false);
            var contentResult = await asyncResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.Equal(string.Empty, contentResult);
        }

        [Fact]
        public async Task SetResponseContent_ForGivenString_ContentIsExpectedString()
        {
            // Arrange
            var httpClientMockBuilder = new HttpClientMockBuilder();
            var httpClient = httpClientMockBuilder.SetResponseContent("test").Build();

            // Act
            var asyncResult = await httpClient.GetAsync("https://www.foo.bar.com/").ConfigureAwait(false);
            var contentResult = await asyncResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.Equal("test", contentResult);
        }

        [Fact]
        public async Task SetResponseStatusCode_ForGivenStatusCode_ExpectedStatusCode()
        {
            // Arrange
            var httpClientMockBuilder = new HttpClientMockBuilder();
            httpClientMockBuilder.SetResponseStatusCode(HttpStatusCode.Ambiguous);
            var httpClient = httpClientMockBuilder.Build();

            // Act
            var asyncResult = await httpClient.GetAsync("https://www.foo.bar.com/").ConfigureAwait(false);
            var statusCode = asyncResult.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.Ambiguous, statusCode);
        }
    }
}
