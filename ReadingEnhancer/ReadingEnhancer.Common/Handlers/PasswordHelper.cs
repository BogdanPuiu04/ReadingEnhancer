using System.Security.Cryptography;
using System.Text;

namespace ReadingEnhancer.Common.Handlers;

public static class PasswordHelper
{
    public static bool VerifyPassword(string givenPassword, string actualPassword)
    {
        if (givenPassword == null)
            throw new ArgumentException("Given password cant be null.");
        var hashPassword = ComputePasswordHash(givenPassword);
        return hashPassword.ToLower().Equals(actualPassword.ToLower());
    }

    private static string ByteArrayToString(byte[] arrInput)
    {
        int i;
        var sOutput = new StringBuilder(arrInput.Length);
        for (i = 0; i < arrInput.Length - 1; i++)
        {
            sOutput.Append(arrInput[i].ToString("X2"));
        }

        return sOutput.ToString();
    }

    public static string ComputePasswordHash(string password)
    {
        var tmpSource = Encoding.ASCII.GetBytes(password);
        var tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
        return ByteArrayToString(tmpHash);
    }
}