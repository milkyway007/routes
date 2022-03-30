using Application;
using Application.Interfaces;
using NUnit.Framework;
using Persistence;
using Persistence.Interfaces;

namespace Tests.Unit.Logic
{
    [TestFixture]
    public class LatitudeLongitudeDistanceCalculatorTests
    {
        private IDistanceCalculator _subject;
        private IAirportRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var fileReader = new FileReader();
            var dataProvider = new CsvParser(fileReader);

            _repository = new AirportRepository(dataProvider);

            _subject = new LatitudeLongitudeDistanceCalculator();
        }

        [Test]
        public void Calculate_RealData_ShouldCalculate()
        {
            //Arrange
            const double DELTA = 1.0;

            var asf = _repository.GetByCodes("ASF", "URWA");
            var kzn = _repository.GetByCodes("KZN", "UWKD");
            var cek = _repository.GetByCodes("CEK", "USCC");
            var ovb = _repository.GetByCodes("OVB", "UNNT");
            var svx = _repository.GetByCodes("SVX", "USSS");
            var dme = _repository.GetByCodes("DME", "UUDD");
            var nbc = _repository.GetByCodes("NBC", "UWKE");
            var gyd = _repository.GetByCodes("GYD", "UBBB");
            var fco = _repository.GetByCodes("FCO", "LIRF");
            var ego = _repository.GetByCodes("EGO", "UUOB");
            var kgd = _repository.GetByCodes("KGD", "UMKK");
            var txl = _repository.GetByCodes("TXL", "EDDT");
            var aoi = _repository.GetByCodes("AOI", "LIPY");
            var dus = _repository.GetByCodes("DUS", "EDDL");
            var led = _repository.GetByCodes("LED", "ULLI");
            var uua = _repository.GetByCodes("UUA", "UWKB");
            var njc = _repository.GetByCodes("NJC", "USNN");
            var svo = _repository.GetByCodes("SVO", "UUEE");
            var arh = _repository.GetByCodes("ARH", "ULAA");
            var uak = _repository.GetByCodes("UAK", "BGBW");
            var zmt = _repository.GetByCodes("ZMT", "CZMT");

            //Act
            var d1 = _subject.Calculate(asf, kzn);
            var d2 = _subject.Calculate(cek, ovb);
            var d3 = _subject.Calculate(ovb, svx);
            var d4 = _subject.Calculate(dme, nbc);
            var d5 = _subject.Calculate(nbc, gyd);

            var d6 = _subject.Calculate(gyd, fco);
            var d7 = _subject.Calculate(ego, kgd);
            var d8 = _subject.Calculate(kgd, txl);
            var d9 = _subject.Calculate(txl, aoi);
            var d10 = _subject.Calculate(aoi, dus);

            var d11 = _subject.Calculate(led, uua);
            var d12 = _subject.Calculate(uua, njc);
            var d13 = _subject.Calculate(njc, svo);
            var d14 = _subject.Calculate(svo, arh);
            var d15 = _subject.Calculate(uak, zmt);

            //Assert
            Assert.AreEqual(1040, decimal.ToDouble(d1), DELTA);
            Assert.AreEqual(1339, decimal.ToDouble(d2), DELTA);
            Assert.AreEqual(1370, decimal.ToDouble(d3), DELTA);
            Assert.AreEqual(892, decimal.ToDouble(d4), DELTA);
            Assert.AreEqual(1685, decimal.ToDouble(d5), DELTA);

            Assert.AreEqual(3144, decimal.ToDouble(d6), DELTA);
            Assert.AreEqual(1172, decimal.ToDouble(d7), DELTA);
            Assert.AreEqual(546, decimal.ToDouble(d8), DELTA);
            Assert.AreEqual(994, decimal.ToDouble(d9), DELTA);
            Assert.AreEqual(986, decimal.ToDouble(d10), DELTA);

            Assert.AreEqual(1464, decimal.ToDouble(d11), DELTA);
            Assert.AreEqual(1557, decimal.ToDouble(d12), DELTA);
            Assert.AreEqual(2301, decimal.ToDouble(d13), DELTA);
            Assert.AreEqual(976, decimal.ToDouble(d14), DELTA);
        }
    }
}
