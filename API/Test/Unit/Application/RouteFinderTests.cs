using Application.Interfaces;
using Domain;
using Domain.Interfaces;
using Application;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;
using System.Collections.Generic;

namespace Tests.Unit.Logic
{
    [TestFixture]
    public class RouteFinderTests
    {
        private IRouteFinder _subject;
        private Mock<IAirportRepository> _airportRepository;
        private Mock<IGraphRepresentationCreator> _graphRepresentationCreator;
        private Mock<ISearch> _search;
        private Mock<IDistanceCalculator> _distanceCalculator;

        [SetUp]
        public void SetUp()
        {
            _airportRepository = new Mock<IAirportRepository>();
            _graphRepresentationCreator = new Mock<IGraphRepresentationCreator>();
            _search = new Mock<ISearch>();
            _distanceCalculator = new Mock<IDistanceCalculator>();

            _subject = new RouteFinder(
                _airportRepository.Object,
                _graphRepresentationCreator.Object,
                _search.Object,
                _distanceCalculator.Object);
        }

        [Test]
        public void Find_GroundChangeAbsent_ShouldCallGetAirportByCodesTwice()
        {
            //Arrange
            _ = _airportRepository.Setup(x => x.GetByCodes(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((IAirport)null);

            //Act
            _subject.Find(("AAA", "BBB"), ("CCC", "DDD"));

            //Assert
            _airportRepository.Verify(x => x.GetByCodes(It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(2));
        }

        [Test]
        public void Find_SourceIsNull_ShouldNotSearch()
        {
            //Arrange
            _ = _airportRepository.Setup(x => x.GetByCodes("AAA", "BBB"))
                .Returns((IAirport)null);
            _ = _airportRepository.Setup(x => x.GetByCodes("CCC", "DDD"))
                .Returns(new Airport());

            //Act
            _subject.Find(("AAA", "BBB"), ("CCC", "DDD"));

            //Assert
            _search.Verify(x => x.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IGraphRepresentation>()),
                Times.Never);
        }

        [Test]
        public void Find_DestinationIsNull_ShouldNotSearch()
        {
            //Arrange
            _ = _airportRepository.Setup(x => x.GetByCodes("AAA", "BBB"))
                .Returns(new Airport());
            _ = _airportRepository.Setup(x => x.GetByCodes("CCC", "DDD"))
                .Returns((IAirport)null);

            //Act
            _subject.Find(("AAA", "BBB"), ("CCC", "DDD"));

            //Assert
            _search.Verify(x => x.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IGraphRepresentation>()),
                Times.Never);
        }

        [Test]
        public void Find_SourceIsNull_ShouldReturnRouteResult()
        {
            //Arrange
            const int expectedLegCount = -1;
            const decimal expectedDistance = 0;
            var expectedDestination = new Airport
            {
                Id = 1,
            };
            _ = _airportRepository.Setup(x => x.GetByCodes("AAA", "BBB"))
                .Returns((IAirport)null);
            _ = _airportRepository.Setup(x => x.GetByCodes("CCC", "DDD"))
                .Returns(expectedDestination);

            //Act
            var actual = _subject.Find(("AAA", "BBB"), ("CCC", "DDD"));

            //Assert
            Assert.NotNull(actual);
            Assert.IsInstanceOf<RouteResult>(actual);
            Assert.Null(actual.Source);
            Assert.AreEqual(expectedDestination.Id, actual.Destination.Id);

            Assert.AreEqual(expectedLegCount, actual.LegCount);
            Assert.Null(actual.Path);
            Assert.AreEqual(expectedDistance, actual.DistanceInKm);
        }

        [Test]
        public void Find_DestinationIsNull_ShouldReaturnRouteResult()
        {
            //Arrange
            const int expectedLegCount = -1;
            const decimal expectedDistance = 0;
            var expectedSource = new Airport
            {
                Id = 1,
            };
            _ = _airportRepository.Setup(x => x.GetByCodes("AAA", "BBB"))
                .Returns(expectedSource);
            _ = _airportRepository.Setup(x => x.GetByCodes("CCC", "DDD"))
                .Returns((IAirport)null);

            //Act
            var actual = _subject.Find(("AAA", "BBB"), ("CCC", "DDD"));

            //Assert
            Assert.NotNull(actual);
            Assert.IsInstanceOf<RouteResult>(actual);
            Assert.Null(actual.Destination);
            Assert.AreEqual(expectedSource.Id, actual.Source.Id);

            Assert.AreEqual(expectedLegCount, actual.LegCount);
            Assert.Null(actual.Path);
            Assert.AreEqual(expectedDistance, actual.DistanceInKm);
        }

        [Test]
        public void Find_SourceDestinationNotNull_ShouldFetchGraphRepresentation()
        {
            //Arrange
            _ = _airportRepository.Setup(x => x.GetByCodes(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Airport());

            Mock<IGraphRepresentation> graphRepresentation = new();
            _ = _graphRepresentationCreator.SetupGet(x => x.GraphRepresentation)
                .Returns(graphRepresentation.Object);

            var searchResult = new Mock<ISearchResult>();
            searchResult.SetupGet(x => x.GroundChangeSourceId).Returns(-1);
            _ = _search.Setup(x =>
            x.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IGraphRepresentation>()))
                .Returns(searchResult.Object);

            //Act
            _subject.Find(("AAA", "BBB"), ("CCC", "DDD"));

            //Assert
            _graphRepresentationCreator.VerifyGet(x => x.GraphRepresentation, Times.Once);
        }

        [Test]
        public void Find_SourceDestinationNotNull_ShouldSearch()
        {
            //Arrange
            _ = _airportRepository.Setup(x => x.GetByCodes(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Airport());

            Mock<IGraphRepresentation> graphRepresentation = new Mock<IGraphRepresentation>();
            _ = _graphRepresentationCreator.SetupGet(x => x.GraphRepresentation)
                .Returns(graphRepresentation.Object);

            var searchResult = new Mock<ISearchResult>();
            searchResult.SetupGet(x => x.GroundChangeSourceId).Returns(-1);
            _ = _search.Setup(x =>
            x.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IGraphRepresentation>()))
                .Returns(searchResult.Object);

            //Act
            _subject.Find(("AAA", "BBB"), ("CCC", "DDD"));

            //Assert
            _search.Verify(x => 
            x.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IGraphRepresentation>()), Times.Once);
        }

        [Test]
        public void Find_SourceDestinationNotNull_ShouldReturnRouteResult()
        {
            //Arrange
            const int legCount = 4;
            const decimal distance = 4000;
            var source = new Airport
            {
                Id = 1,
            };
            var destination = new Airport
            {
                Id = 5,
            };
            var path = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
            };

            _ = _airportRepository.Setup(x => x.GetByCodes("AAA", "BBB"))
                 .Returns(source);
            _ = _airportRepository.Setup(x => x.GetByCodes("CCC", "DDD"))
                .Returns(destination);

            Mock<IGraphRepresentation> graphRepresentation = new Mock<IGraphRepresentation>();
            _ = _graphRepresentationCreator.SetupGet(x => x.GraphRepresentation)
                .Returns(graphRepresentation.Object);

            var searchResult = new Mock<ISearchResult>();
            searchResult.SetupGet(x => x.LegCount).Returns(legCount);
            searchResult.SetupGet(x => x.DistanceInKm).Returns(distance);
            searchResult.SetupGet(x => x.Path).Returns(path);
            searchResult.SetupGet(x => x.GroundChangeSourceId).Returns(-1);
            _ = _search.Setup(x =>
            x.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IGraphRepresentation>()))
                .Returns(searchResult.Object);

            //Act
            var actual = _subject.Find(("AAA", "BBB"), ("CCC", "DDD"));

            //Assert
            Assert.NotNull(actual);
            Assert.IsInstanceOf<RouteResult>(actual);
            Assert.NotNull(actual.Source);
            Assert.NotNull(actual.Destination);
            Assert.AreEqual(source.Id, actual.Source.Id);
            Assert.AreEqual(destination.Id, actual.Destination.Id);
            Assert.AreEqual(legCount, actual.LegCount);
            Assert.AreEqual(distance, actual.DistanceInKm);
            CollectionAssert.AreEqual(path, actual.Path);
        }

        [Test]
        public void Find_GroundChangeAbsent_ShouldReturnRouteResult()
        {
            //Arrange
           _ = _airportRepository.Setup(x => x.GetByCodes("AAA", "BBB"))
                 .Returns(new Airport());
            _ = _airportRepository.Setup(x => x.GetByCodes("CCC", "DDD"))
                .Returns(new Airport());

            Mock<IGraphRepresentation> graphRepresentation = new Mock<IGraphRepresentation>();
            _ = _graphRepresentationCreator.SetupGet(x => x.GraphRepresentation)
                .Returns(graphRepresentation.Object);

            var searchResult = new Mock<ISearchResult>();
            searchResult.SetupGet(x => x.GroundChangeSourceId).Returns(-1);
            _ = _search.Setup(x =>
            x.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IGraphRepresentation>()))
                .Returns(searchResult.Object);

            //Act
            var actual = _subject.Find(("AAA", "BBB"), ("CCC", "DDD"));

            //Assert
            Assert.Null(actual.GroundChange);
        }

        [Test]
        public void Find_GroundChangePresent_ShouldCallGetAirportByIdTwice()
        {
            //Arrange
            const int groundChangeSourceId = 3;
            var path = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
            };

            _ = _airportRepository.Setup(x => x.GetByCodes(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Airport());
            _ = _airportRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Airport());
            _ = _distanceCalculator.Setup(x => x.Calculate(It.IsAny<IAirport>(), It.IsAny<IAirport>()))
                .Returns(50);

            Mock<IGraphRepresentation> graphRepresentation = new Mock<IGraphRepresentation>();
            _ = _graphRepresentationCreator.SetupGet(x => x.GraphRepresentation)
                .Returns(graphRepresentation.Object);

            var searchResult = new Mock<ISearchResult>();
            searchResult.SetupGet(x => x.Path).Returns(path);
            searchResult.SetupGet(x => x.GroundChangeSourceId).Returns(groundChangeSourceId);
            _ = _search.Setup(x =>
            x.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IGraphRepresentation>()))
                .Returns(searchResult.Object);

            //Act
            _subject.Find(("AAA", "BBB"), ("CCC", "DDD"));

            //Assert
            _airportRepository.Verify(x => x.GetById(It.IsAny<int>()),
                Times.Exactly(2));
        }

        [Test]
        public void Find_GroundChangePresent_ShouldCalculateGroundChangeDistance()
        {
            //Arrange
            const int groundChangeSourceId = 3;
            var path = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
            };

            _ = _airportRepository.Setup(x => x.GetByCodes(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Airport());
            _ = _airportRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Airport());
            _ = _distanceCalculator.Setup(x => x.Calculate(It.IsAny<IAirport>(), It.IsAny<IAirport>()))
                .Returns(50);

            Mock<IGraphRepresentation> graphRepresentation = new Mock<IGraphRepresentation>();
            _ = _graphRepresentationCreator.SetupGet(x => x.GraphRepresentation)
                .Returns(graphRepresentation.Object);

            var searchResult = new Mock<ISearchResult>();
            searchResult.SetupGet(x => x.Path).Returns(path);
            searchResult.SetupGet(x => x.GroundChangeSourceId).Returns(groundChangeSourceId);
            _ = _search.Setup(x =>
            x.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IGraphRepresentation>()))
                .Returns(searchResult.Object);

            //Act
            _subject.Find(("AAA", "BBB"), ("CCC", "DDD"));

            //Assert
            _distanceCalculator.Verify(x => x.Calculate(It.IsAny<IAirport>(), It.IsAny<IAirport>()),
                Times.Once);
        }

        [Test]
        public void Find_GroundChangePresent_ShouldReturnRouteResult()
        {
            //Arrange
            const decimal expectedGroundChangeDistance = 50;
            const int groundChangeSourceId = 3;
            var path = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
            };

            _ = _airportRepository.Setup(x => x.GetByCodes(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Airport());
            _ = _airportRepository.Setup(x => x.GetById(3))
                .Returns(new Airport());
            _ = _airportRepository.Setup(x => x.GetById(4))
               .Returns(new Airport());
            _ = _distanceCalculator.Setup(x => x.Calculate(It.IsAny<IAirport>(), It.IsAny<IAirport>()))
                .Returns(expectedGroundChangeDistance);

            var graphRepresentation = new Mock<IGraphRepresentation>();
            _ = _graphRepresentationCreator.SetupGet(x => x.GraphRepresentation)
                .Returns(graphRepresentation.Object);

            var searchResult = new Mock<ISearchResult>();
            searchResult.SetupGet(x => x.Path).Returns(path);
            searchResult.SetupGet(x => x.GroundChangeSourceId).Returns(groundChangeSourceId);
            _ = _search.Setup(x =>
            x.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IGraphRepresentation>()))
                .Returns(searchResult.Object);

            //Act
            var actual = _subject.Find(("AAA", "BBB"), ("CCC", "DDD"));

            //Assert
            Assert.NotNull(actual.GroundChange);
            Assert.NotNull(actual.GroundChange.Source);
            Assert.NotNull(actual.GroundChange.Destination);
            Assert.AreEqual(expectedGroundChangeDistance, actual.GroundChange.DistanceInKm);
        }
    }
}
