using Application;
using Application.Interfaces;
using NUnit.Framework;
using Persistence;
using System;
using System.Linq;

namespace Tests.Integration.Persistence
{
    [TestFixture]
    public class RouteFinderTests
    {
        private IRouteFinder _subject;

        [SetUp]
        public void SetUp()
        {
            var fileReader = new FileReader();
            var dataProvider = new CsvParser(fileReader);

            var distanceCalculator = new LatitudeLongitudeDistanceCalculator();
            var airportRepository = new AirportRepository(dataProvider);
            var routeRepository = new RouteRepository(dataProvider);

            var graphRepresentationCreator = new AdjacencyDictionaryCreator(
                distanceCalculator, airportRepository, routeRepository);
            graphRepresentationCreator.Create();
            var search = new DijkstraSearch();            

            _subject = new RouteFinder(
                airportRepository, graphRepresentationCreator, search, distanceCalculator);
        }

        [Test]
        public void Find_RealDataOption1_ShouldFind()
        {
            //Arrange
            const int maxDistance = 1040;
            const int maxPathLength = 2;

            //Act
            var result = _subject.Find(("ASF", "URWA"), ("KZN", "UWKD"));

            //Assert
            Assert.LessOrEqual(Math.Round(result.DistanceInKm), maxDistance);
            Assert.LessOrEqual(result.Path.Count(), maxPathLength);
        }

        [Test]
        public void Find_RealDataOption2_ShouldFind()
        {
            //Arrange
            const int maxDistance = 2709;
            const int maxPathLength = 3;

            const int expectedDistance = 1494;
            const int expectedPathLength = 3;

            //Act
            var result = _subject.Find(("CEK", "USCC"), ("SVX", "USSS"));

            //Assert
            Assert.LessOrEqual(Math.Round(result.DistanceInKm), maxDistance);
            Assert.LessOrEqual(result.Path.Count(), maxPathLength);

            Assert.AreEqual(expectedDistance, Math.Round(result.DistanceInKm));
            Assert.AreEqual(expectedPathLength, result.Path.Count());
        }

        [Test]
        public void Find_RealDataOption3_ShouldFind()
        {
            //Arrange
            const int maxDistance = 5721;
            const int maxPathLength = 4;

            const int expectedDistance = 2395;
            const int expectedPathLength = 2;

            //Act
            var result = _subject.Find(("DME", "UUDD"), ("FCO", "LIRF"));

            //Assert
            Assert.LessOrEqual(Math.Round(result.DistanceInKm), maxDistance);
            Assert.LessOrEqual(result.Path.Count(), maxPathLength);

            Assert.AreEqual(expectedDistance, Math.Round(result.DistanceInKm));
            Assert.AreEqual(expectedPathLength, result.Path.Count());
        }

        [Test]
        public void Find_RealDataOption4_ShouldFind()
        {
            //Arrange
            const int maxDistance = 3698;
            const int maxPathLength = 5;

            const int expectedDistance = 2186;
            const int expectedPathLength = 4;

            //Act
            var result = _subject.Find(("EGO", "UUOB"), ("DUS", "EDDL"));

            //Assert
            Assert.LessOrEqual(Math.Round(result.DistanceInKm), maxDistance);
            Assert.LessOrEqual(result.Path.Count(), maxPathLength);

            Assert.AreEqual(expectedDistance, Math.Round(result.DistanceInKm));
            Assert.AreEqual(expectedPathLength, result.Path.Count());
        }

        [Test]
        public void Find_RealDataOption5_ShouldFind()
        {
            //Arrange
            const int maxDistance = 6298;
            const int maxPathLength = 5;

            const int expectedDistance = 759;
            const int expectedPathLength = 2;

            //Act
            var result = _subject.Find(("LED", "ULLI"), ("ARH", "ULAA"));

            //Assert
            Assert.LessOrEqual(Math.Round(result.DistanceInKm), maxDistance);
            Assert.LessOrEqual(result.Path.Count(), maxPathLength);

            Assert.AreEqual(expectedDistance, Math.Round(result.DistanceInKm));
            Assert.AreEqual(expectedPathLength, result.Path.Count());
        }
    }
}
