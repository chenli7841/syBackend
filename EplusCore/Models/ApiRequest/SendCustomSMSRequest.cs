namespace WebUI.Models.ApiRequest
{
    public class SendCustomSMSRequest
    {
        public int RecipientUserId { get; set; }
        public string Message { get; set; }
    }
}
