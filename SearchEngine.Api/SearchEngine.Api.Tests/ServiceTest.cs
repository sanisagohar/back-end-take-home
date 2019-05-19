using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchEngine.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Api.Tests
{
    [TestClass]
    public class ServiceTest
    {
        Service _service;

        public ServiceTest()
        {
            _service = new Service();
        }
        [TestMethod]
        public void GetShortestPathNoPathExists()
        {
            // Arrange
            var request = new Core.Dto.SearchRequestDto { Origin = "CRW", Destination = "NRT" };

            // Act
            var response = _service.FindShortestRoute(request);

            // Assert
            Assert.IsNull(response.Result);
            Assert.AreEqual("No flight found between 'Yeager Airport,United States,Charleston,CRW' and 'Narita International Airport,Japan,Tokyo,NRT'", response.Message);
        }

        [TestMethod]
        public void GetShortestPathEmptyParameter()
        {
            // Arrange
            var request = new Core.Dto.SearchRequestDto { Origin = "", Destination = "NRT" };

            // Act
            var response = _service.FindShortestRoute(request);

            // Assert
            Assert.IsNull(response.Result);
            Assert.AreEqual(false, response.Success);
            Assert.AreEqual("Both origin and destination must be provided", response.Message);
        }

        [TestMethod]
        public void GetShortestPathDestinationNoFound()
        {
            // Arrange
            var request = new Core.Dto.SearchRequestDto { Origin = "CRW", Destination = "Saudia Arabia" };

            // Act
            var response = _service.FindShortestRoute(request);

            // Assert
            Assert.IsNull(response.Result);
            Assert.AreEqual(false, response.Success);
            Assert.AreEqual("Destination cannot be found", response.Message);
        }

        [TestMethod]
        public void GetShortestPathWithIATA3()
        {
            // Arrange
            var request = new Core.Dto.SearchRequestDto { Origin = "ABJ", Destination = "COO" };

            // Act
            var response = _service.FindShortestRoute(request);

            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(2, response.Result.Count());
            Assert.AreEqual("Air China (ABJ - BRU)", response.Result.ElementAt(0));
            Assert.AreEqual("Air China (BRU - COO)", response.Result.ElementAt(1));
        }

        [TestMethod]
        public void GetShortestPathWithName()
        {
            // Arrange
            var request = new Core.Dto.SearchRequestDto { Origin = "Port Bouet Airport", Destination = "Denver International Airport" };

            // Act
            var response = _service.FindShortestRoute(request);

            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(3, response.Result.Count());
            Assert.AreEqual("Air China (ABJ - BRU)", response.Result.ElementAt(0));
            Assert.AreEqual("Air China (BRU - YUL)", response.Result.ElementAt(1));
            Assert.AreEqual("Air China (YUL - DEN)", response.Result.ElementAt(2));
        }

        [TestMethod]
        public void GetShortestPathWithCountry()
        {
            // Arrange
            var request = new Core.Dto.SearchRequestDto { Origin = "Barbados", Destination = "Cote d'Ivoire" };

            // Act
            var response = _service.FindShortestRoute(request);

            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(4, response.Result.Count());
            Assert.AreEqual("Air China (BGI - YYZ)", response.Result.ElementAt(0));
            Assert.AreEqual("Air China (YYZ - EWR)", response.Result.ElementAt(1));
            Assert.AreEqual("United Airlines (EWR - BRU)", response.Result.ElementAt(2));
            Assert.AreEqual("United Airlines (BRU - ABJ)", response.Result.ElementAt(3));
        }

        [TestMethod]
        public void GetShortestPathWithCity()
        {
            // Arrange
            var request = new Core.Dto.SearchRequestDto { Origin = "Abidjan", Destination = "Bridgetown" };

            // Act
            var response = _service.FindShortestRoute(request);

            // Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(3, response.Result.Count());
            Assert.AreEqual("Air China (ABJ - BRU)", response.Result.ElementAt(0));
            Assert.AreEqual("Air China (BRU - YYZ)", response.Result.ElementAt(1));
            Assert.AreEqual("Air China (YYZ - BGI)", response.Result.ElementAt(2));
        }

        [TestMethod]
        public void GetShortestPathWithAnyString()
        {
            // Arrange
            var request = new Core.Dto.SearchRequestDto { Origin = "Abidjan", Destination = "kn" };

            // Act
            var response = _service.FindShortestRoute(request);

            // Assert
            Assert.AreEqual("Records fetched successfully between 'Port Bouet Airport,Cote d'Ivoire,Abidjan,ABJ' and 'Yellowknife Airport,Canada,Yellowknife,YZF'", response.Message);
            Assert.AreEqual(4, response.Result.Count());
            Assert.AreEqual("Air China (ABJ - BRU)", response.Result.ElementAt(0));
            Assert.AreEqual("Air China (BRU - YUL)", response.Result.ElementAt(1));
            Assert.AreEqual("Air China (YUL - YEG)", response.Result.ElementAt(2));
            Assert.AreEqual("Air China (YEG - YZF)", response.Result.ElementAt(3));
        }
    }
}
