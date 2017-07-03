using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    using PostcodeParser.UK;

    using Xunit;

    public class ValidationTests
    {
        [Fact]
        public void FailsNullPostcode()
        {
            var result = Postcode.IsValid(null);
            Assert.Equal(false, result);
        }

        [Fact]
        public void FailEmptyPostcode()
        {
            var result = Postcode.IsValid("");
            Assert.Equal(false, result);
        }

        [Fact]
        public void FailsWhitespacePostcode()
        {
            var result = Postcode.IsValid(null);
            Assert.Equal(false, result);
        }

        [Fact]
        public void FailsAllNumbers()
        {
            var result = Postcode.IsValid("111 111");
            Assert.Equal(false, result);
        }

        [Fact]
        public void FailsAllText()
        {
            var result = Postcode.IsValid("AAA AAA");
            Assert.Equal(false, result);
        }

        [Fact]
        public void FailsInvalidArea()
        {
            var result = Postcode.IsValid("11W 0NY");
            Assert.Equal(false, result);
        }
        
        [Fact]
        public void FailsInvalidDistrict()
        {
            var result = Postcode.IsValid("SWWW 0NY");
            Assert.Equal(false, result);
        }

        [Fact]
        public void FailsInvalidSector()
        {
            var result = Postcode.IsValid("SW1W ANY");
            Assert.Equal(false, result);
        }

        [Fact]
        public void FailsInvalidUnit()
        {
            var result = Postcode.IsValid("SW1W 099");
            Assert.Equal(false, result);
        }

        [Fact]
        public void SantaIsValid()
        {
            var result = Postcode.IsValid("XM4 5HQ");
            Assert.Equal(true, result);
        }

        // Could Test for some  the following, but it is unclear how much of this is fixed and how much is just an observation:
        /*
         As all formats end with 9AA, the first part of a postcode can easily be extracted by ignoring the last three characters
Areas with only single-digit districts: BR, FY, HA, HD, HG, HR, HS, HX, JE, LD, SM, SR, WC, WN, ZE (although WC is always subdivided by a further letter, e.g. WC1A).
Areas with only double-digit districts: AB, LL, SO.
Areas with a district '0' (zero): BL, BS, CM, CR, FY, HA, PR, SL, SS (BS is the only area to have both a district 0 and a district 10).
The following central London single-digit districts have been further divided by inserting a letter after the digit and before the space: EC1–EC4 (but not EC50), SW1, W1, WC1, WC2, and part of E1 (E1W), N1 (N1C and N1P), NW1 (NW1W) and SE1 (SE1P).
The letters QVX are not used in the first position.
The letters IJZ are not used in the second position.
The only letters to appear in the third position are ABCDEFGHJKPSTUW when the structure starts with A9A.
The only letters to appear in the fourth position are ABEHMNPRVWXY when the structure starts with AA9A.
The final two letters do not use the letters CIKMOV, so as not to resemble digits or each other when hand-written.
Post code sectors are one of ten digits: 0 to 9 with 0 only used once 9 has been used in a post town, save for Croydon and Newport (see above).
         */

    }
}
