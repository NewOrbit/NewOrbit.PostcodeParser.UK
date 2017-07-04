# UK Postcode parsing
This is a simple package to break a UK postcode into its constituent parts.
It's really just a simple wrapper around this regex:
`(^[A-Z]{1,2})([0-9]{1,2}[A-Z]{0,1})\\s?([0-9])([A-Z]{2}$) `

This repository just adds some testing around it.

UK postcodes can be broken into parts thus:

<table>
    <thead>
        <tr><th>Postcode</th><th colspan=2>Outward code</th><th colspan=2>Inward code</th>
        </tr>
        <tr><th>&nbsp;</th><th>Area</th><th>District</th><th>Sector</th><th>Unit</th> </tr>
    </thead>
    <tbody>
        <tr><th>M1 1AA  </th><td>M </td><td> 1</td><td>1</td><td>AA</td></tr>
        <tr><th>M60 1NW </th><td>M </td><td>60</td><td>1</td><td>NW</td></tr>
        <tr><th>CR2 6XH </th><td>CR</td><td> 2</td><td>6</td><td>XH</td></tr>
        <tr><th>DN55 1PT</th><td>DN</td><td>55</td><td>1</td><td>PT</td></tr>
        <tr><th>W1P 1BB </th><td>W </td><td>1P</td><td>1</td><td>BB</td></tr>
        <tr><th>EC1A 1BB</th><td>EC</td><td>1A</td><td>1</td><td>BB</td></tr>
    </tbody>
</table>

# Installation
`Install-Package PostcodeParser.UK`

# Usage
`Postcode.Destructure("EC1A 1BB")` will return the breakdown per the table above.
It will throw an `ArgumentException` if the postcode is not in a valid format.

`Postcode.IsValid("EC1A 1BB")` will return `true` if the format of the postcode is valid, otherwise it will return `false`.

The space in the middle of the postcode is optional. However, only zero or one spaces are accepted; more than one space is invalid. Similarly, leading or trailing spaces are invalid.

The parser has been tested with the full list of UK postcodes as downloaded from https://www.freemaptools.com/download-uk-postcode-lat-lng.htm. The BruteParse console app in the test folder can be used to test this.

# Performance
The Regex is compiled the first time you call either method, meaning there is a delay of a few tens of ms on the first call. Subsequent calls should perform in sub-ms time.

If you run the BruteParse app on the full 1.7 million UK postcodes, it should complete in a few seconds.

# Not supported postcodes
There are certain special cases as outlined in https://en.wikipedia.org/wiki/Postcodes_in_the_United_Kingdom#Special_cases that are not supported, e.g.:
- Overseas Territories
- British Forces Post Office (BFPO)
- Girobank

PRs to add support more than welcome. Part of the problem is to figure out how to parse them. For example, Ascension Island used "ASCN 1ZZ" and it is not clear how or if you should split "ASCN". If anyone can find the specification for that, please let me know. Anguilla and Virgin Islands, for example, compound the problem by having different layouts altogether.

However, the following special cases *are* supported.
- The Crown Dependencies, i.e. the Isle of Man and the Channel Islands (Jersey and Guernsey (which includes Alderney, Sark, and some other islands)), as they follow the standard rules
- Channel Islands seem to follow the standard rules so are not really a special case (?)
- Santa Claus / Father Christmas (XM4 5HQ)
