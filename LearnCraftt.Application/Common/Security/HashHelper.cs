using System.Security.Cryptography;
using System.Text;

namespace LearnCraftt.Application.Common.Security;

public class HashHelper
{
    public static string Hash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hashBytes);
    }
}