namespace PostcodeParser.UK
{
    /// <summary>
    /// A postcode consists of
    /// Outward.Area - 1-2 letters
    /// Outward.Distring - 1-2 digits or a digit followed by a letter
    /// Inward.Sector - a single digit
    /// Inward.Unit - two letters
    /// </summary>
    public class DestructuredPostcode
    {
        public string Postcode => $"{this.Outward.Area}{this.Outward.District} {this.Inward.Sector}{this.Inward.Unit}";

        public Outward Outward { get; set; } = new Outward();

        public Inward Inward {  get; set; } = new Inward();
    }

    public struct Outward
    {
        /// <summary>
        /// An Area Code can be one or two letters, e.g. ‘B’ or ‘BA’ 
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Postcode district can be one or two digits or a digit followed by a letter, eg 1, 11, 1A
        /// </summary>
        public string District { get; set; }
    }

    public struct Inward
    {
        /// <summary>
        /// Postcode sector is a single digit
        /// </summary>
        public short Sector { get; set; }

        /// <summary>
        /// Unit is two letters
        /// </summary>
        public string Unit { get; set; }
    }
}