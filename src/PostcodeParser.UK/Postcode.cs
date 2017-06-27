namespace PostcodeParser.UK
{
    using System;
    using System.Text.RegularExpressions;

    public static class Postcode
    {
        private const string pattern = "(^[A-Z]{1,2})([0-9]{1,2}[A-Z]{0,1})([0-9])([A-Z]{2}$)";
        private static readonly Lazy<Regex> LazyRegex = new Lazy<Regex>(() => new Regex(pattern, RegexOptions.Compiled));

        private static Regex matcher = LazyRegex.Value;

        public static DestructuredPostcode Destructure(string postcode)
        {
            postcode = postcode.Replace(" ", "").ToUpperInvariant();

            var match = matcher.Match(postcode);
            return new DestructuredPostcode()
                       {
                           Outward = new Outward() { Area = match.Groups[1].Value, District = match.Groups[2].Value},
                           Inward = new Inward() { Sector = Convert.ToInt16(match.Groups[3].Value), Unit = match.Groups[4].Value}
                       };
        }
    }
}