namespace Domain.Models
{
    public class RouteUserPermission
    {
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public bool IsVisible { get; set; }
    }
}
