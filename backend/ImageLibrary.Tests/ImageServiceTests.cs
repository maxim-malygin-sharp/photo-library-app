using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ImageLibrary.Domain;
using ImageLibrary.Domain.Models;
using ImageLibrary.Domain.Services;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ImageLibrary.Tests
{
    [TestFixture]
    public class ImageServiceTests
    {
        private IImageService _service;
        private Mock<IHttpClientFactory> _clientFactoryMock;

        [SetUp]
        public void SetUp()
        {
            _clientFactoryMock = new Mock<IHttpClientFactory>();
            
            _service = new ImageService(_clientFactoryMock.Object, new AppConfig { ImageLibraryApi = "http://testapi" } );
        }

        [Test(Description = "Receive data when status is OK")]
        public async Task ReceiveData_When_Response_Ok_Test()
        {
            var albumId = 1;
            var data = new List<AlbumImageDTO> {
                new AlbumImageDTO { AlbumId = albumId, Id = 1, Title = "image title 1" },
                new AlbumImageDTO { AlbumId = albumId, Id = 2, Title = "image title 2" }
            };

            var content = JsonConvert.SerializeObject(data);
            var clientHandlerStub = new HttpHandlerDelegateStub((request, cancellationToken) => {
                var response = new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content)
                };
                return Task.FromResult(response);
            });

            _clientFactoryMock.Setup(m => m.CreateClient(It.IsAny<string>()))
                .Returns(() => new HttpClient(clientHandlerStub));

            Mock<IHttpClientFactory> clientFactoryMock = new Mock<IHttpClientFactory>(MockBehavior.Strict);
            clientFactoryMock
                .Setup(x => x.CreateClient("test"))
                .Returns(new HttpClient(clientHandlerStub))
                .Verifiable();

            var result = await _service.GetImagesByAlbumIdAsync(albumId);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), data.Count);
            Assert.AreEqual(result.First().Id, data[0].Id);
            Assert.AreEqual(result.First().Title, data[0].Title);
        }

        [Test(Description = "Receive empty data when status is OK")]
        public async Task GetEmpty_When_ResponseIsEmpty_Test()
        {
            var albumId = 1;
            var data = new List<AlbumImageDTO>();

            var content = JsonConvert.SerializeObject(data);
            var clientHandlerStub = new HttpHandlerDelegateStub((request, cancellationToken) => {
                var response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content)
                };
                return Task.FromResult(response);
            });

            _clientFactoryMock.Setup(m => m.CreateClient(It.IsAny<string>()))
                .Returns(() => new HttpClient(clientHandlerStub));

            Mock<IHttpClientFactory> clientFactoryMock = new Mock<IHttpClientFactory>(MockBehavior.Strict);
            clientFactoryMock
                .Setup(x => x.CreateClient("test"))
                .Returns(new HttpClient(clientHandlerStub))
                .Verifiable();

            var result = await _service.GetImagesByAlbumIdAsync(albumId);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, data.Count);
        }

        [Test(Description = "Throws exception data when response status is bad")]
        public async Task ThrowsException_IfResponse_IsNotSuccessful_Test()
        {
            var albumId = 1;
            
            var clientHandlerStub = new HttpHandlerDelegateStub((request, cancellationToken) => {
                var response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent(string.Empty)
                };
                return Task.FromResult(response);
            });

            _clientFactoryMock.Setup(m => m.CreateClient(It.IsAny<string>()))
                .Returns(() => new HttpClient(clientHandlerStub));

            Mock<IHttpClientFactory> clientFactoryMock = new Mock<IHttpClientFactory>(MockBehavior.Strict);
            clientFactoryMock
                .Setup(x => x.CreateClient("test"))
                .Returns(new HttpClient(clientHandlerStub))
                .Verifiable();

            var isException = false;
            
            try
            {
                await _service.GetImagesByAlbumIdAsync(albumId);
            }
            catch
            {
                isException = true;
            }

            Assert.IsTrue(isException);
        }
    }
}
