using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace App.Models
{


    public class CryptoUtils
    {
        public static readonly RSAEncryptionPadding DefaultRsaPadding = RSAEncryptionPadding.OaepSHA256;
        private readonly RSA _rsa;
        private readonly Aes _aes;

        public CryptoUtils()
        {
            _rsa = RSA.Create(2048);
            _aes = Aes.Create();
            _aes.Mode = CipherMode.CBC;
            _aes.Padding = PaddingMode.Zeros;
        }

        public byte[] RsaPublicKey => _rsa.ExportRSAPublicKey();
        public byte[] AesKey
        {
            get => _aes.Key;
            set => _aes.Key = value;
        }
        public byte[] AesIV
        {
            get => _aes.IV;
            set => _aes.IV = value;
        }

        public byte[] EncryptRsa(byte[] data) => _rsa.Encrypt(data, DefaultRsaPadding);
        public byte[] DecryptRsa(byte[] data) => _rsa.Decrypt(data, DefaultRsaPadding);

        public byte[] EncryptAes(byte[] data)
        {
            var encryptor = _aes.CreateEncryptor();
            using var msEncrypt = new MemoryStream();
            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(Encoding.UTF8.GetString(data));
            }
            return msEncrypt.ToArray();
        }

        public byte[] DecryptAes(byte[] data)
        {
            var decryptor = _aes.CreateDecryptor();
            using var msEncrypt = new MemoryStream(data);
            using var csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read);
            using var srEncrypt = new StreamReader(csEncrypt);
            var text = srEncrypt.ReadToEnd();
            while (text.Length > 0 && text[^1] == '\u0000')
            {
                text = text[..^1];
            }
            return Encoding.UTF8.GetBytes(text);
        }
    }
}
