namespace Tests
{
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
            var result = Postcode.Destructure(postcode);
            Assert.Equal(area, result.Outward.Area);
            Assert.Equal(district, result.Outward.District);
            Assert.Equal(sector, result.Inward.Sector);
            Assert.Equal(unit, result.Inward.Unit);
        }
    }
}
