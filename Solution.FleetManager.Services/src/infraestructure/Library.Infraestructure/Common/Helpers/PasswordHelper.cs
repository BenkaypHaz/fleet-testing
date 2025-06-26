using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHelper
{
    private const int SaltSize = 16; 
    private const int HashSize = 32; 
    private const int Iterations = 4; 
    private const int MemorySize = 65536; 
    private const int DegreeOfParallelism = 2;

    public static string HashPassword(string password)
    {
        var salt = new byte[SaltSize];
        RandomNumberGenerator.Fill(salt);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            Iterations = Iterations,
            MemorySize = MemorySize,
            DegreeOfParallelism = DegreeOfParallelism
        };

        var hash = argon2.GetBytes(HashSize);

        var hashBase64 = Convert.ToBase64String(hash);
        var saltBase64 = Convert.ToBase64String(salt);

        return $"{hashBase64}:{saltBase64}";
    }

    public static bool VerifyPassword(string password, string stored)
    {
        var parts = stored.Split(':');
        if (parts.Length != 2) return false;

        var originalHash = Convert.FromBase64String(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            Iterations = Iterations,
            MemorySize = MemorySize,
            DegreeOfParallelism = DegreeOfParallelism
        };

        var computedHash = argon2.GetBytes(HashSize);

        return CryptographicOperations.FixedTimeEquals(originalHash, computedHash);
    }
}
