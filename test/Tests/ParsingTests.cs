namespace Tests
{
    using System;

    using PostcodeParser.UK;

    using Xunit;

    public class ParsingTests
    {
        [Theory]
        [InlineData("M1 1AA", "M", "1", 1, "AA")]
        [InlineData("M60 1NW", "M", "60", 1, "NW")]
        [InlineData("CR2 6XH", "CR", "2", 6, "XH")]
        [InlineData("DN55 1PT", "DN", "55", 1, "PT")]
        [InlineData("W1P 1BB", "W", "1P", 1, "BB")]
        [InlineData("EC1A 1BB", "EC", "1A", 1, "BB")]
        [InlineData("M11AA", "M", "1", 1, "AA")]
        [InlineData("M601NW", "M", "60", 1, "NW")]
        [InlineData("CR26XH", "CR", "2", 6, "XH")]
        [InlineData("DN551PT", "DN", "55", 1, "PT")]
        [InlineData("W1P1BB", "W", "1P", 1, "BB")]
        [InlineData("EC1A1BB", "EC", "1A", 1, "BB")]
        public void CanParse(string postcode, string area, string district, int sector, string unit)
        {
            TestParsing(postcode, area, district, sector, unit);
        }

        private static void TestParsing(string postcode, string area, string district, int sector, string unit)
        {
            var result = Postcode.Destructure(postcode);
            Assert.Equal(area, result.Outward.Area);
            Assert.Equal(district, result.Outward.District);
            Assert.Equal(sector, result.Inward.Sector);
            Assert.Equal(unit, result.Inward.Unit);
        }

        
        // https://en.wikipedia.org/wiki/Postcodes_in_the_United_Kingdom#Special_cases


        [Theory]
        [InlineData("JE2 3LP", "JE", "2", 3, "LP")] 
        [InlineData("GY1 1FJ", "GY", "1", 1, "FJ")] 
        public void CanHandleChannelIslands(string postcode, string area, string district, int sector, string unit)
        {
            TestParsing(postcode, area, district, sector, unit);
        }

        [Theory]
        [InlineData("IM1 2EL", "IM", "1", 2, "EL")]
        public void CanHandleIsleOfMan(string postcode, string area, string district, int sector, string unit)
        {
            TestParsing(postcode, area, district, sector, unit);
        }

        [Fact]
        public void CanHandleSanta()
        {
            TestParsing("XM4 5HQ", "XM", "4", 5, "HQ");
        }

        [Theory]
        [InlineData("BS98 1TL", "BS", "98", 1, "TL")]
        [InlineData("DE99 3GG", "DE", "99", 3, "GG")]
        [InlineData("L30 4GB", "L", "30", 4, "GB")]
        [InlineData("S2 4SU", "S", "2", 4, "SU")]
        [InlineData("W1N 4DJ", "W", "1N", 4, "DJ")]
        public void CanHandleSpecialPostcodes(string postcode, string area, string district, int sector, string unit)
        {
            TestParsing(postcode, area, district, sector, unit);
        }

        [Theory]
        [InlineData("ZZ99 3CZ", "ZZ", "99", 3, "CZ")]
        public void CanHandleNHS(string postcode, string area, string district, int sector, string unit)
        {
            TestParsing(postcode, area, district, sector, unit);
        }

        [Fact]
        public void ThrowsOnInvalidCode()
        {
            Assert.Throws<ArgumentException>(() => Postcode.Destructure("111 111"));
        }


    }
}
