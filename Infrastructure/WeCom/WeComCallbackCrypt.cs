using System;
using System.Buffers.Binary;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using Common.WeCom;

namespace Infrastructure.WeCom
{
    public class WeComCallbackCrypt : IWeComCallbackCrypt
    {
        private readonly WeComOptions _options;

        public WeComCallbackCrypt(WeComOptions options)
        {
            _options = options;
        }

        public string VerifyUrl(string signature, string timestamp, string nonce, string encryptedEcho)
        {
            VerifySignature(signature, timestamp, nonce, encryptedEcho);
            return Decrypt(encryptedEcho);
        }

        public string DecryptMessage(string signature, string timestamp, string nonce, string encryptedXml)
        {
            var envelope = XDocument.Parse(encryptedXml);
            var encrypted = envelope.Descendants("Encrypt").FirstOrDefault()?.Value;
            if (string.IsNullOrWhiteSpace(encrypted)) throw new CryptographicException("企业微信回调中缺少 Encrypt 字段。");
            VerifySignature(signature, timestamp, nonce, encrypted);
            return Decrypt(encrypted);
        }

        private void VerifySignature(string signature, string timestamp, string nonce, string encrypted)
        {
            EnsureConfigured();
            var values = new[] { _options.CallbackToken, timestamp, nonce, encrypted };
            Array.Sort(values, StringComparer.Ordinal);
            using var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(values)));
            var expected = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            var actualBytes = Encoding.ASCII.GetBytes(signature ?? string.Empty);
            var expectedBytes = Encoding.ASCII.GetBytes(expected);
            if (actualBytes.Length != expectedBytes.Length || !CryptographicOperations.FixedTimeEquals(actualBytes, expectedBytes))
                throw new CryptographicException("企业微信回调签名无效。");
        }

        private string Decrypt(string encrypted)
        {
            var key = Convert.FromBase64String(_options.CallbackEncodingAesKey + "=");
            var cipher = Convert.FromBase64String(encrypted);
            byte[] plain;
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = key.Take(16).ToArray();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.None;
                using var decryptor = aes.CreateDecryptor();
                plain = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);
            }

            plain = RemovePkcs7Padding(plain);
            if (plain.Length < 20) throw new CryptographicException("企业微信回调明文长度无效。");
            var messageLength = BinaryPrimitives.ReadUInt32BigEndian(plain.AsSpan(16, 4));
            if (messageLength > plain.Length - 20) throw new CryptographicException("企业微信回调消息长度无效。");
            var message = Encoding.UTF8.GetString(plain, 20, checked((int)messageLength));
            var receiveId = Encoding.UTF8.GetString(plain, 20 + checked((int)messageLength), plain.Length - 20 - checked((int)messageLength));
            if (!string.Equals(receiveId, _options.CorpId, StringComparison.Ordinal)) throw new CryptographicException("企业微信回调 CorpId 不匹配。");
            return message;
        }

        private static byte[] RemovePkcs7Padding(byte[] value)
        {
            var padding = value[value.Length - 1];
            if (padding < 1 || padding > 32 || padding > value.Length) throw new CryptographicException("企业微信回调填充无效。");
            for (var i = value.Length - padding; i < value.Length; i++)
                if (value[i] != padding) throw new CryptographicException("企业微信回调填充无效。");
            return value.Take(value.Length - padding).ToArray();
        }

        private void EnsureConfigured()
        {
            if (string.IsNullOrWhiteSpace(_options.CallbackToken)) throw new InvalidOperationException("尚未配置 WeCom:CallbackToken。");
            if (string.IsNullOrWhiteSpace(_options.CallbackEncodingAesKey) || _options.CallbackEncodingAesKey.Length != 43)
                throw new InvalidOperationException("WeCom:CallbackEncodingAesKey 必须为 43 个字符。");
        }
    }
}
