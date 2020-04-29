using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTM2LATSqlServerLibrary;

namespace UTM2LATSqlServerLibrary.Tests
{
    [TestClass]
    public class UTM2LATSqlServerLibraryTests
    {

        [TestMethod]
        public void Compute_UTM2LAT_ShouldReturnCorrectLatitude()
        {
            double XUTM = 723399.51;
            double YUTM = 4373328.5;

            // Act
            double latitude = UTM2LATSqlServerLibrary.UTM2LAT(XUTM, YUTM);

            // Assert
            Assert.AreEqual<double>(expected: 39.480565745305434, actual: latitude);
        }

        [TestMethod]
        public void Compute_UTM2LONG_ShouldReturnCorrectLongitude()
        {
            double XUTM = 723399.51;
            double YUTM = 4373328.5;

            // Act
            double longitude = UTM2LATSqlServerLibrary.UTM2LONG(XUTM, YUTM);

            // Assert
            Assert.AreEqual<double>(expected: -0.40259272724511247, actual: longitude);
        }

        [TestMethod]
        public void Compute_UTM2LATITUDE_ShouldReturnCorrectLatitude()
        {
            double XUTM = 723399.51;
            double YUTM = 4373328.5;
            string LatBand = "S";
            int LongBand = 30;

            // Act
            double latitude = UTM2LATSqlServerLibrary.UTM2LATITUDE(XUTM, YUTM, LatBand, LongBand);

            // Assert
            Assert.AreEqual<double>(expected: 39.480565745305434, actual: latitude);
        }

        [TestMethod]
        public void Compute_UTM2LONGITUDE_ShouldReturnCorrectLongitude()
        {
            double XUTM = 723399.51;
            double YUTM = 4373328.5;
            string LatBand = "S";
            int LongBand = 30;

            // Act
            double longitude = UTM2LATSqlServerLibrary.UTM2LONGITUDE(XUTM, YUTM, LatBand, LongBand);

            // Assert
            Assert.AreEqual<double>(expected: -0.40259272724511247, actual: longitude);
        }

    }
    
}
