namespace AOC.util;

public static class StringUtils
{
    /// <summary>
    /// Returns the number of characters that are different between two strings of the same length.
    /// </summary>
    /// <exception cref="InvalidDataException">Throws this if the strings are not the same length.</exception>
    public static int StringDistance(string a, string b)
    {
        if (a.Length != b.Length)
        {
            throw new InvalidDataException();
        }
        return a == b ? 0 : a.Zip(b).Count(x => x.First != x.Second);
    }

    /// <summary>
    /// Transposes characters of strings. Assumes strings are the same length. Will effectively rotate "left".
    /// </summary>
    public static string[] TransposeStrings(string[] source)
    {
        // rotate "left"
        var sWidth = source[0].Length; // will become height
        var sHeight = source.Length; // will become width

        var result = new string[sWidth];
        for (var y = 0; y < sWidth; y++)
        {
            result[y] = "";
            for (var x = 0; x < sHeight; x++)
            {
                result[y] += source[x][y];
            }
        }

        return result;
    }
}