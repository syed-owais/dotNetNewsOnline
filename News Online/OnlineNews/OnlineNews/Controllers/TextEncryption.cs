using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OnlineNews.Controllers.backend
{
    public class TextEncryption
    {
        public string TextEncryp(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strbuild = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strbuild.Append(result[i].ToString("x2"));
            }
            return strbuild.ToString();
        }
    }
}