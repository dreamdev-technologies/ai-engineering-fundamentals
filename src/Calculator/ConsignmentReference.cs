using System.Security.Cryptography;
using System.Text;

namespace Calculator;

/// <summary>Short stable identifiers for consignments, used in file names and log lines.</summary>
public static class ConsignmentReference
{
    /// <summary>An eight-character fingerprint of a reference.</summary>
    public static string Fingerprint(string reference)
    {
        ArgumentNullException.ThrowIfNull(reference);
        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(reference));
        return Convert.ToHexString(hash)[..8];
    }
}
