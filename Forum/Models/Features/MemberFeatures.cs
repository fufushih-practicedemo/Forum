using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Forum.Models.Features
{
    public class MemberFeatures
    {
        public string HashPassword(string Password)
        {
            string saltKey = "jdfaoifjafhf812383109Q";
            string saltAndPassword = String.Concat(Password, saltKey);

            // Define SHA1
            SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();

            // tansform password to byte data
            byte[] PasswordData = Encoding.Default.GetBytes(saltAndPassword);
            byte[] HashData = sha1Hasher.ComputeHash(PasswordData);

            string HashResult = "";

            for(int i= 0; i < HashData.Length; i++) {
                HashResult += HashData[i].ToString("x2");
            }

            return HashResult;
        }
    }
}