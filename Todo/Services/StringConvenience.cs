using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Todo.Services
{
    public static class StringConvenience
    {
        public static string GetHash(string str)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(str.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }
    }
}