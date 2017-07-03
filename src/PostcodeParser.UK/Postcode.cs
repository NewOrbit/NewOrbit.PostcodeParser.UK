namespace PostcodeParser.UK
{
    using System;
    using System.Text.RegularExpressions;

    public static class Postcode
    {
        private const string Pattern = "(^[A-Z]{1,2})([0-9]{1,2}[A-Z]{0,1})\\s?([0-9])([A-Z]{2}$)";
        private static readonly Lazy<Regex> LazyRegex = new Lazy<Regex>(() => new Regex(Pattern, RegexOptions.Compiled));

        private static readonly Regex Matcher = LazyRegex.Value;

        public static DestructuredPostcode Destructure(string postcode)
        {
            if (string.IsNullOrWhiteSpace(postcode))
            {
                throw new ArgumentException("Postcode should not be empty", nameof(postcode));
            }

            var match = Matcher.Match(postcode);
            if (!IsValid(match))
            {
                throw new ArgumentException($"{postcode} is not a valid postcode", nameof(postcode));
            }

            return new DestructuredPostcode()
                       {
                           Outward = new Outward() { Area = match.Groups[1].Value, District = match.Groups[2].Value},
                           Inward = new Inward() { Sector = Convert.ToInt16(match.Groups[3].Value), Unit = match.Groups[4].Value}
                       };
        }

        private static bool IsValid(Match postcode)
        {
            if (postcode.Groups.Count != 5)
            {
                return false;
            }

            return true;
        }

        public static bool IsValid(string postcode)
        {
            if (string.IsNullOrWhiteSpace(postcode)) return false;
            var match = Matcher.Match(postcode);
            return IsValid(match);
        }
    }
}