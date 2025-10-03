using System.Collections.Generic;
using ClosedXML.Excel;

namespace Infrastructure.Exporters
{
    interface IExcelExporter<in T>
    {
        XLWorkbook Export(IEnumerable<T> orders);
    }
}
