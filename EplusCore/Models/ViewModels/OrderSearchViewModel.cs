using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models.ViewModels
{
    public class OrderSearchViewModel
    {
        public IEnumerable<CompanyEntity> Companies { get; set; }
        public IEnumerable<OrderStateEntity> AllOrderStates { get; set; }

    }
}
