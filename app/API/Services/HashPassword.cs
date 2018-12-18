using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace API.Services
{
    public static class HashPassword
    {
        private static (string hashed, string salt) Hash (string password, string saltData = null)
        {
                byte[] salt;
                if (saltData == null) {
                    salt = new byte[128/8];
                    using (var rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(salt);
                    }
                } else 
                {
                    salt = Convert.FromBase64String(saltData);
                }
                string hashed = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256/8
                    )
                );
                //Console.Write($"\tReal: {password}\n\tSalt: {Convert.ToBase64String(salt)}\n\tHashed: {hashed}\n\tTogether: {password+'.'+Convert.ToBase64String(salt)}\n\n");
                return (hashed, Convert.ToBase64String(salt));
        }

        public static Task<(string hashed, string salt)> HashAsync (string password, string saltData = null)
        {
            return Task.Run(() => Hash(password, saltData));
        }
    }
}