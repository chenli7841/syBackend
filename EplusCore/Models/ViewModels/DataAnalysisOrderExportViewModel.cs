using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models.ViewModels
{
    public class DataAnalysisOrderExportViewModel
    {
        public IEnumerable<DataAnalysisOrderEntity> Entities { get; set; }
        public IEnumerable<DataAnalysisOrderSummary> RecipientSummary { get; set; }
        public IEnumerable<DataAnalysisOrderSummary> AgentSummary { get; set; }
        public IEnumerable<DataAnalysisOrderSummary> LocationSummary { get; set; }
    }
}
