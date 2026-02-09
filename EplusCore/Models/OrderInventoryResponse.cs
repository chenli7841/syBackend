using Domain.Entities;
using Domain.Enums;
using System.Collections;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class OrderInventoryResponse
    {
        public OrderState OrderState { get; set; }
        public IEnumerable<CompanyEntity> Companies { get; set; }
        public string CompanyIds { get; set; }
    }
}
