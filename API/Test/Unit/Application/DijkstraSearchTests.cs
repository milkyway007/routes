using Application;
using Application.Interfaces;
using NUnit.Framework;
using System.Linq;

namespace Tests.Logic
{
    [TestFixture]
    public class DijkstraSearchTests
    {
        private ISearch _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new DijkstraSearch();
        }

        [Test]
        public void Search_SmallGraphOption1_ShouldFindShortestPathToAllNodes()
        {
            //Arrange
            const int sourceId = 0;
            
            decimal[][] matrix = {
                new decimal[] { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
                new decimal[] { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
                new decimal[] { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
                new decimal[] { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
                new decimal[] { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
                new decimal[] { 0, 0, 4, 0, 10, 0, 2, 0, 0 },
                new decimal[] { 0, 0, 0, 14, 0, 2, 0, 1, 6 },
                new decimal[] { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
                new decimal[] { 0, 0, 2, 0, 0, 0, 6, 7, 0 },
            };

            var graph = new AdjacencyDictionary(Enumerable.Range(0,9));
            for (int j = 0; j < matrix.Length; j++)
            {
                for (int k = 0; k < matrix[j].Length; k++)
                {
                    graph.AddEdge(j, k, matrix[j][k] * 1000);
                }
            }

            //Act, Assert
            var destinationId = 1;
            var result = _subject.Search(sourceId, destinationId, graph);
            var expectedDistance = 4000m;
            var actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace);

            destinationId = 2;
            result = _subject.Search(sourceId, destinationId, graph);
            expectedDistance = 12000m;
            actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace);

            destinationId = 3;
            result = _subject.Search(sourceId, destinationId, graph);
            expectedDistance = 19000m;
            actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace);
            destinationId = 4;
            result = _subject.Search(sourceId, destinationId, graph);
            expectedDistance = 21000m;
            actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace); 

            destinationId = 5;
            result = _subject.Search(sourceId, destinationId, graph);
            expectedDistance = 11000m;
            actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace);
            
            destinationId = 6;
            result = _subject.Search(sourceId, destinationId, graph);
            expectedDistance = 9000m;
            actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace);

            destinationId = 7;
            result = _subject.Search(sourceId, destinationId, graph);
            expectedDistance = 8000m;
            actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace);

            destinationId = 8;
            result = _subject.Search(sourceId, destinationId, graph);
            expectedDistance = 14000m;
            actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace);
        }

        [Test]
        public void Search_SmallGraphOption2_ShouldFindShortestPathToNode()
        {
            //Arrange
            const int sourceId = 0;
            const int destinationId = 2;

            int[] expectedPath = { 0, 3, 2 };

            decimal[][] matrix = {
                new decimal[]{ 0, 0, 0, 3, 12 },
                new decimal[]{ 0, 0, 2, 0, 0 },
                new decimal[]{ 0, 0, 0, 3, 0 },
                new decimal[]{ 0, 5, 3, 0, 9 },
                new decimal[]{ 0, 0, 7, 0, 0 },
            };

            var graph = new AdjacencyDictionary(Enumerable.Range(0, 5));
            for (int j = 0; j < matrix.Length; j++)
            {
                for (int k = 0; k < matrix[j].Length; k++)
                {
                    graph.AddEdge(j, k, matrix[j][k] * 1000);
                }
            }

            //Act
            var result = _subject.Search(sourceId, destinationId, graph);

            //Assert
            CollectionAssert.AreEqual(expectedPath, result.Path);
        }

        [Test]
        public void Search_SmallGraphOption3_ShouldFindShortestPathToAllNodes()
        {
            //Arrange
            const int sourceId = 0;

            decimal[][] matrix = {
                new decimal[]{ 0, 11, 4, 20, 0, 0 },
                new decimal[]{ 0, 0, 0, 6, 0, 0  },
                new decimal[]{ 0, 2, 0, 15, 7, 0  },
                new decimal[]{ 0, 0, 0, 0, 0, 5  },
                new decimal[]{ 0, 0, 0, 3, 0, 0  },
                new decimal[]{ 0, 0, 0, 0, 0, 0  },
            };

            var graph = new AdjacencyDictionary(Enumerable.Range(0, 6));
            for (int j = 0; j < matrix.Length; j++)
            {
                for (int k = 0; k < matrix[j].Length; k++)
                {
                    graph.AddEdge(j, k, matrix[j][k] * 1000);
                }
            }

            //Act, Assert
            var destinationId = 1;
            var result = _subject.Search(sourceId, destinationId, graph);
            var expectedDistance = 6000m;
            var actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace);

            destinationId = 2;
            result = _subject.Search(sourceId, destinationId, graph);
            expectedDistance = 4000m;
            actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace);

            destinationId = 3;
            result = _subject.Search(sourceId, destinationId, graph);
            expectedDistance = 12000m;
            actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace);
            destinationId = 4;
            result = _subject.Search(sourceId, destinationId, graph);
            expectedDistance = 11000m;
            actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace);

            destinationId = 5;
            result = _subject.Search(sourceId, destinationId, graph);
            expectedDistance = 17000m;
            actualDistace = result.DistanceInKm;

            Assert.AreEqual(expectedDistance, actualDistace);
        }

        [Test]
        public void Search_SmallGraphOption4_ShouldFindShortestPathToNode()
        {
            //Arrange
            const int sourceId = 0;
            const int destinationId = 3;
            const decimal expectedDistance = 12000m;
            const int expectedPathLength = 3;
            int[] expectedPath = { 0, 4, 3 };

            decimal[][] matrix = {
                new decimal[]{ 0, 1, decimal.MaxValue, decimal.MaxValue, 10 },
                new decimal[]{ 1, 0, 4, decimal.MaxValue, decimal.MaxValue },
                new decimal[]{ decimal.MaxValue, 4, 0, 7, decimal.MaxValue },
                new decimal[]{ decimal.MaxValue, decimal.MaxValue, 7, 0, 2 },
                new decimal[]{ 10, decimal.MaxValue, decimal.MaxValue, 2, 0 },
            };

            var graph = new AdjacencyDictionary(Enumerable.Range(0, 5));
            for (int j = 0; j < matrix.Length; j++)
            {
                for (int k = 0; k < matrix[j].Length; k++)
                {
                    var value = matrix[j][k];
                    if (value < decimal.MaxValue)
                    {
                        graph.AddEdge(j, k, matrix[j][k] * 1000);
                    }
                }
            }

            //Act
            var result = _subject.Search(sourceId, destinationId, graph);

            //Assert
            Assert.AreEqual(expectedDistance, result.DistanceInKm);
            Assert.AreEqual(expectedPathLength, result.Path.Count());
            CollectionAssert.AreEqual(expectedPath, result.Path);
        }

        [Test]
        public void Search_SmallGraphWithGroundChange_ShouldFindShortestPathToNode()
        {
            //Arrange
            const int sourceId = 0;
            const int destinationId = 5;
            const int expectedDistance = 541;
            const int expectedGroundChangeSourceId = 4;
            int[] expectedPath = { 0, 2, 4, 3, 6, 5 };

            decimal[][] matrix = {
                new decimal[] { 0, 600, 150, decimal.MaxValue, decimal.MaxValue, decimal.MaxValue, decimal.MaxValue },
                new decimal[] { decimal.MaxValue, 0, 250, decimal.MaxValue, decimal.MaxValue, 125, decimal.MaxValue },
                new decimal[] { decimal.MaxValue, decimal.MaxValue, 0, 250, 125,decimal.MaxValue, decimal.MaxValue },
                new decimal[] { decimal.MaxValue, 110, decimal.MaxValue, 0, decimal.MaxValue, 250, 101},
                new decimal[] { decimal.MaxValue, decimal.MaxValue, decimal.MaxValue, 50, 0, decimal.MaxValue, decimal.MaxValue },
                new decimal[] { decimal.MaxValue, decimal.MaxValue, decimal.MaxValue, decimal.MaxValue, decimal.MaxValue, 0, decimal.MaxValue },
                new decimal[] { decimal.MaxValue, decimal.MaxValue, decimal.MaxValue, decimal.MaxValue, decimal.MaxValue, 115, 0 },
            };

            var graph = new AdjacencyDictionary(Enumerable.Range(0, 9).ToArray());
            for (int j = 0; j < matrix.Length; j++)
            {
                for (int k = 0; k < matrix[j].Length; k++)
                {
                    graph.AddEdge(j, k, matrix[j][k]);
                }
            }

            //Act
            var result = _subject.Search(sourceId, destinationId, graph);

            //Assert
            Assert.AreEqual(expectedDistance, result.DistanceInKm);
            Assert.AreEqual(expectedGroundChangeSourceId, result.GroundChangeSourceId);
            CollectionAssert.AreEqual(expectedPath, result.Path);
        }
    }
}
