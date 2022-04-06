using Application;
using Application.Interfaces;
using Domain;
using NUnit.Framework;


namespace Tests.Logic
{
    [TestFixture]
    public class LatitudeLongitudeDistanceCalculatorTests
    {
        private IDistanceCalculator _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new LatitudeLongitudeDistanceCalculator();
        }

        [Test]
        public void Calculate_Option1_ShouldCalculate()
        {
            //Arrange
            const decimal expected = 1544;
            const double delta = 5.0;

            var a = AirportBuilder.Init()
                .SetId("1")
                .SetLatitude("10")
                .SetLongitude("20")
                .Build();
            var b = AirportBuilder.Init()
                .SetId("2")
                .SetLatitude("20")
                .SetLongitude("10")
                .Build();

            //Act
            var actual = _subject.Calculate(a, b);

            //Assert
            Assert.AreEqual(decimal.ToDouble(expected), decimal.ToDouble(actual), delta);
        }

        [Test]
        public void Calculate_Option2_ShouldCalculate()
        {
            //Arrange
            const decimal expected = 5004;
            const double delta = 5.0;

            var a = AirportBuilder.Init()
                .SetId("1")
                .SetLatitude("45")
                .SetLongitude("90")
                .Build();
            var b = AirportBuilder.Init()
                .SetId("2")
                .SetLatitude("90")
                .SetLongitude("45")
                .Build();

            //Act
            var actual = _subject.Calculate(a, b);

            //Assert
            Assert.AreEqual(decimal.ToDouble(expected), decimal.ToDouble(actual), delta);
        }

        [Test]
        public void Calculate_Option3_ShouldCalculate()
        {
            //Arrange
            const decimal expected = 16150;
            const double delta = 5.0;

            var a = AirportBuilder.Init()
                .SetId("1")
                .SetLatitude("-65")
                .SetLongitude("-135")
                .Build();
            var b = AirportBuilder.Init()
                .SetId("2")
                .SetLatitude("65")
                .SetLongitude("135")
                .Build();

            //Act
            var actual = _subject.Calculate(a, b);

            //Assert
            Assert.AreEqual(decimal.ToDouble(expected), decimal.ToDouble(actual), delta);
        }

        [Test]
        public void Calculate_Option4_ShouldCalculate()
        {
            //Arrange
            const decimal expected = 19790;
            const double delta = 5.0;

            var a = AirportBuilder.Init()
                .SetId("1")
                .SetLatitude("-89")
                .SetLongitude("179")
                .Build();
            var b = AirportBuilder.Init()
                .SetId("2")
                .SetLatitude("89")
                .SetLongitude("-179")
                .Build();

            //Act
            var actual = _subject.Calculate(a, b);

            //Assert
            Assert.AreEqual(decimal.ToDouble(expected), decimal.ToDouble(actual), delta);
        }
    }
}