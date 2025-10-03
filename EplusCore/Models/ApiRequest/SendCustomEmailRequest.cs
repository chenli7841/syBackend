namespace WebUI.Models.ApiRequest
{
    public class SendCustomEmailRequest
    {
        public int RecipientUserId { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
    }
}
