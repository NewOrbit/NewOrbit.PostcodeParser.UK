# UK Postcode parsing
This is a simple package to break a UK postcode into it's constituent parts.
It's really just a simple wrapper around this regex:
`(^[A-Z]{1,2})([0-9]{1,2}[A-Z]{0,1})\\s?([0-9])([A-Z]{2}$) `

This repository just adds some testing around it.

UK postcodes can be broken into parts like thus:

|Postcode|Outward code |Inward code|
|--------|-------------|-----------|
|        |Area|District|Sector|Unit|
|--------|:--:|:------:|:----:|:--:|
|M1 1AA  |M   |1       |1     |AA  |
|M60 1NW |M   |60      |1     |NW  |
|CR2 6XH |CR  |2       |6     |XH  |
|DN55 1PT|DN  |55      |1     |PT  |
|W1P 1BB |W   |1P      |1     |BB  |
|EC1A 1BB|EC  |1A      |1     |BB  |

# Usage
`Postcode.Destructure("EC1A 1BB")` will return the breakdown as per the table.
It will thrown an `ArgumentException` if the postcode is not in a valid format.

`Postcode.IsValid("EC1A 1BB")` will return `true` if the format of the postcode is valid, otherwise it will return `false`.

The space in the middle of the postcode is optional. However, only zero or one spaces is accepted; More than one space is invalid. Similarly, leading or trailing spaces are invalid.

The parser has been tested with the full list of UK postcodes as downloaded from https://www.freemaptools.com/download-uk-postcode-lat-lng.htm. The BruteParse console app in the test folder can be used to test this. 

# Performance
The Regex is compiled the first time you call either method, meaning there is a delay of a few tens of ms on the first call. Subsequent calls should perform in sub-ms time.

If you run the BruteParse app on the full 1.7 million UK postcodes, you should it complete in a few seconds.

# Not supported postcodes
There are certain special cases as outlined in https://en.wikipedia.org/wiki/Postcodes_in_the_United_Kingdom#Special_cases that are not supported:
- Overseas Territories
- British Forces Post Office (BFPO)
- Girobank


PRs to add support more than welcome. Part of the problem is to figure out how to parse them. For example, Ascension Island used "ASCN 1ZZ" and it is not clear how or if you should split "ASCN". If anyone can find the specification for that, please let me know. Anguilla and Virgin Islands, for example, compound the problem by having different layouts altogether.

However, the following special cases *are* supported.
- The Crown Dependencies, i.e. the Isle of Man and the Channel Islands (ie.Jersey and Guernsey (which includes Alderney, Sark and some other islands)) as they follow the standard rules
- Channel Islands seem to follow the standard rules so are not really a special case (?)
- Santa Claus / Father Christmas (XM4 5HQ)
