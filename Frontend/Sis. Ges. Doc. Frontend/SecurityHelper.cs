using System;
using System.Security.Cryptography;
using System.Text;

namespace Sis.Ges.Doc.Frontend
{
    public static class SecurityHelper
    {
        public static string Sha256Hash(string value)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
                StringBuilder builder = new StringBuilder(bytes.Length * 2);

                foreach (byte item in bytes)
                {
                    builder.Append(item.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
