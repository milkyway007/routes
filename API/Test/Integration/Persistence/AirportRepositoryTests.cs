using NUnit.Framework;
using Persistence;

namespace Tests.Logic
{
    [TestFixture]
    public class AirportRepositoryTests
    {
        [Test]
        public void ShouldGetAirport()
        {
            var fileReader = new FileReader();
            var dataProvider = new CsvParser(fileReader);
            var repository = new AirportRepository(dataProvider);

            var asf = repository.GetByCodes("ASF", "URWA");
            var kzn = repository.GetByCodes("KZN", "UWKD");
            var cek = repository.GetByCodes("CEK", "USCC");
            var ovb = repository.GetByCodes("OVB", "UNNT");
            var svx = repository.GetByCodes("SVX", "USSS");
            var dme = repository.GetByCodes("DME", "UUDD");
            var nbc = repository.GetByCodes("NBC", "UWKE");
            var gyd = repository.GetByCodes("GYD", "UBBB");
            var fco = repository.GetByCodes("FCO", "LIRF");
            var ego = repository.GetByCodes("EGO", "UUOB");
            var kgd = repository.GetByCodes("KGD", "UMKK");
            var txl = repository.GetByCodes("TXL", "EDDT");
            var aoi = repository.GetByCodes("AOI", "LIPY");
            var dus = repository.GetByCodes("DUS", "EDDL");
            var led = repository.GetByCodes("LED", "ULLI");
            var uua = repository.GetByCodes("UUA", "UWKB");
            var njc = repository.GetByCodes("NJC", "USNN");
            var svo = repository.GetByCodes("SVO", "UUEE");
            var arh = repository.GetByCodes("ARH", "ULAA");

            Assert.NotNull(asf);
            Assert.NotNull(kzn);
            Assert.NotNull(cek);
            Assert.NotNull(ovb);
            Assert.NotNull(svx);
            Assert.NotNull(dme);
            Assert.NotNull(nbc);
            Assert.NotNull(gyd);
            Assert.NotNull(fco);
            Assert.NotNull(ego);
            Assert.NotNull(kgd);
            Assert.NotNull(txl);
            Assert.NotNull(aoi);
            Assert.NotNull(dus);
            Assert.NotNull(led);
            Assert.NotNull(uua);
            Assert.NotNull(njc);
            Assert.NotNull(svo);
            Assert.NotNull(arh);
        }
    }
}
