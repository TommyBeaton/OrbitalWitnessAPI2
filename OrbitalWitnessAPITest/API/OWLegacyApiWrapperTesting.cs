using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using OrbitalWitnessAPI.Domain;
using OrbitalWitnessAPI.DTO;
using OrbitalWitnessAPI.API;
using RichardSzalay.MockHttp;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace OrbitalWitnessAPITest.API
{
    public class OWLegacyApiWrapperTesting
    {
        private string _username = "testy";
        private string _password = "mcTestFace";
        private string _prodUri = "https://prodhost:8080";
        private string _devUri = "https://localhost:7203";

        private Mock<IConfiguration> GetMockedConfiguration()
        {
            var usernameSectionMock = new Mock<IConfigurationSection>();
            var passwordSectionMock = new Mock<IConfigurationSection>();
            var prodUriSectionMock = new Mock<IConfigurationSection>();
            var devUriSectionMock = new Mock<IConfigurationSection>();
            usernameSectionMock.Setup(x => x.Value).Returns(_username);
            passwordSectionMock.Setup(x => x.Value).Returns(_password);       
            prodUriSectionMock.Setup(x => x.Value).Returns(_prodUri);
            devUriSectionMock.Setup(x => x.Value).Returns(_devUri);

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x.GetSection("API:OWLegacy:Username")).Returns(usernameSectionMock.Object);
            configurationMock.Setup(x => x.GetSection("API:OWLegacy:Password")).Returns(passwordSectionMock.Object);
            configurationMock.Setup(x => x.GetSection("API:OWLegacy:ProdUri")).Returns(prodUriSectionMock.Object);
            configurationMock.Setup(x => x.GetSection("API:OWLegacy:DevelopmentUri")).Returns(devUriSectionMock.Object);

            return configurationMock;
        }

        #if (!DEBUG)
        //Release Uri
        [Fact]
        public void CorrectDevUriIsFound()
        {
            //arrange
            var configurationMock = GetMockedConfiguration();
            var httpMock = new MockHttpMessageHandler();

            httpMock.Expect(_prodUri + "/schedules");

            var httpFactoryMock = new Mock<IHttpClientFactory>();
            httpFactoryMock.Setup(x => x.CreateClient(string.Empty)).Returns(new HttpClient(httpMock));

            var apiWrapper = new OWLegacyApiWrapper(httpFactoryMock.Object, configurationMock.Object);

            //act
            apiWrapper.GetSchedules();

            //assert
            httpMock.VerifyNoOutstandingExpectation();
        }
        #endif

        #if DEBUG
        [Fact]
        public void CorrectDevUriIsFound()
        {
            //arrange
            var configurationMock = GetMockedConfiguration();
            var httpMock = new MockHttpMessageHandler();

            httpMock.Expect(_devUri + "/schedules");

            var httpFactoryMock = new Mock<IHttpClientFactory>();
            httpFactoryMock.Setup(x => x.CreateClient(string.Empty)).Returns(new HttpClient(httpMock));

            var apiWrapper = new OWLegacyApiWrapper(httpFactoryMock.Object, configurationMock.Object);

            //act
            apiWrapper.GetSchedules();

            //assert
            httpMock.VerifyNoOutstandingExpectation();
        }


        [Fact]
        public void CorrectAuthIsMade()
        {
            //arrange
            string expectedAuth = $"Basic {Convert.ToBase64String(Encoding.Default.GetBytes($"{_username}:{_password}"))}";
            var configurationMock = GetMockedConfiguration();

            var httpMock = new MockHttpMessageHandler();

            httpMock.Expect("/schedules").WithHeaders("Authorization", expectedAuth);

            var httpFactoryMock = new Mock<IHttpClientFactory>();
            httpFactoryMock.Setup(x => x.CreateClient(string.Empty)).Returns(new HttpClient(httpMock));

            var apiWrapper = new OWLegacyApiWrapper(httpFactoryMock.Object, configurationMock.Object);

            //act
            apiWrapper.GetSchedules();

            //assert
            httpMock.VerifyNoOutstandingExpectation();
        }

        #endif
    }
}
