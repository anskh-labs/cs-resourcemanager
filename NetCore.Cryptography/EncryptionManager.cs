using System;
using System.Security.Cryptography;
using System.Text;

namespace NetCore.Cryptography
{
    public class EncryptionManager
    {
        public const string KEY = "@bcdefghijklmnopqrstuvwxyz1234567890#+-=";
        private static EncryptionManager? _instance;
        private EncryptionManager()
        { }
        public static EncryptionManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EncryptionManager();
                return _instance;
            }
        }
        public bool PasswordVerify(string hashedPassword, string plainPassword)
        {
            var a = EncryptString(plainPassword, KEY);
            return hashedPassword.Equals(a);
        }

        public string EncryptString(string Message, string Passphrase)
        {
            UTF8Encoding uTF8Encoding = new UTF8Encoding();
            var md5 = MD5.Create();
            byte[] key = md5.ComputeHash(uTF8Encoding.GetBytes(Passphrase));
            var tripelDES = TripleDES.Create();
            tripelDES.Key = key;
            tripelDES.Mode = CipherMode.ECB;
            tripelDES.Padding = PaddingMode.PKCS7;
            byte[] bytes = uTF8Encoding.GetBytes(Message);
            byte[] inArray;
            try
            {
                ICryptoTransform cryptoTransform = tripelDES.CreateEncryptor();
                inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
            }
            finally
            {
                tripelDES.Clear();
                tripelDES.Clear();
            }
            return Convert.ToBase64String(inArray);
        }
        public string DecryptString(string Message, string Passphrase)
        {
            UTF8Encoding uTF8Encoding = new UTF8Encoding();
            var md5 = MD5.Create();
            byte[] key = md5.ComputeHash(uTF8Encoding.GetBytes(Passphrase));
            var tripleDES = TripleDES.Create();
            tripleDES.Key = key;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            byte[] array = Convert.FromBase64String(Message);
            byte[] bytes;
            try
            {
                ICryptoTransform cryptoTransform = tripleDES.CreateDecryptor();
                bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
            }
            finally
            {
                tripleDES.Clear();
                tripleDES.Clear();
            }
            return uTF8Encoding.GetString(bytes);
        }
    }
}
