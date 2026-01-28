using System.Collections.Generic;
using System.Linq;

namespace WebUI.Models.DataTableRequest
{
    public class DataTableRequestModel
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public List<DataTableColumn> Columns { get; set; }
        public DataTableSearch Search { get; set; }
        public List<DataTableOrder> Order { get; set; }
        public int[] CompanyIds { get; set; }

        public string GetColumnSearchValue(string columnName)
        {
            return Columns.FirstOrDefault(column => column.Name == columnName)?.Search?.Value ?? "";
        }
    }
}
