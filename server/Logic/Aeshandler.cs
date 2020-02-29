using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace server.Logic
{
    public class Aeshandler
    {

        public byte[] aesKey { get; private set; }
        public byte[] aesIv { get; private set; }

        public Aeshandler() {

            //create new key and iv. should be placed in a scure place. fpr example usge, using fixed data.
            using (Aes aes = new AesManaged())
            {
                aes.KeySize = 256;
                aes.GenerateKey();
                aesKey = aes.Key;
                aesIv = aes.IV;
            }

        }

        public byte[] GetAesKey() {
            return aesKey;
        }
        public byte[] GetAesIV() {
            return aesIv;
        }

        public static byte[] ReturnKey() {
            return new byte[] { 27, 22, 154, 58, 186, 121, 57, 102, 232, 188, 23, 130, 160, 23, 51, 124, 137, 44, 254, 117, 170, 84, 229, 152, 97, 80, 36, 204, 216, 132, 168, 114 };
        }

        public static byte[] ReturnIv() {
            return new byte[] { 139, 208, 252, 167, 129, 225, 118, 190, 169, 171, 236, 132, 62, 53, 76, 207 };
        }

        public static string Base64Encode(string iStrToEncode) {

            var plainTextBytes = Encoding.UTF8.GetBytes(iStrToEncode);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string iStrToDecode) {

            var base64EncodedBytes = Convert.FromBase64String(iStrToDecode);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }


        //can handle also files. 
        public static byte[] AESEncryptBytes(byte[] iUncryptedData, byte[] iKey, byte[] iIV)
        {
            byte[] encryptedBytes = null;

            using (Aes aes = new AesManaged())
            {
                aes.Key = iKey;
                aes.IV = iIV;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(iUncryptedData, 0, iUncryptedData.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

        public static byte[] AESDecryptBytes(byte[] iCryptBytes, byte[] iKey, byte[] iIV)
        {
            byte[] clearBytes = null;

            using (Aes aes = new AesManaged())
            {
                aes.Key = iKey;
                aes.IV  = iIV;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(iCryptBytes, 0, iCryptBytes.Length);
                        cs.Close();
                    }
                    clearBytes = ms.ToArray();
                }
            }
            return clearBytes;
        }

        public static string EncryptStr(string iStrToEncrypt) {
            string strFrom64 = Base64Encode(iStrToEncrypt);

            byte[] BytesStr =  Encoding.ASCII.GetBytes(strFrom64);
            var encryptedByes = AESEncryptBytes(BytesStr, ReturnKey(), ReturnIv());

            var encryptedStr = Encoding.ASCII.GetString(encryptedByes);

            string base64Str = Base64Encode(encryptedStr);

            return base64Str;
        }


        public static string DecryptStr(string iStrToEncrypt) {

            string strFrom64 = Base64Decode(iStrToEncrypt);
            byte[] Bytes = Encoding.ASCII.GetBytes(strFrom64);
            var decryptedBytes = AESDecryptBytes(Bytes, ReturnKey(), ReturnIv());
            var decryptedStr = Encoding.ASCII.GetString(decryptedBytes);

            return decryptedStr;
        }










    }
}
