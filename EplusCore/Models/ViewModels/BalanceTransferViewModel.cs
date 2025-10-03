using Domain.Models;

namespace WebUI.Models.ViewModels
{
    public class BalanceTransferViewModel
    {
        public UserDetailViewModel FromUser { get; set; }
        public UserDetailViewModel ToUser { get; set; }
        public BalanceTransferInfo Info { get; set; }
    }
}
