namespace Common.WeCom
{
    public interface IWeComCallbackCrypt
    {
        string VerifyUrl(string signature, string timestamp, string nonce, string encryptedEcho);
        string DecryptMessage(string signature, string timestamp, string nonce, string encryptedXml);
    }
}
