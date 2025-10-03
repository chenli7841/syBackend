using System;
using System.Collections.Generic;
using System.Data;
using ClosedXML.Excel;
using Domain.Entities;

namespace Infrastructure.Exporters
{
    class UserExporter : IExcelExporter<UserEntity>
    {
        private readonly Dictionary<string, Func<UserEntity, string>> _columnMapper =
            new()
            {
                { "用户代码", user => user.Code },
                { "电话", user => user.CanadaPhoneNumber },
                { "邮箱", user => user.Mailbox},
                { "微信", user => user.WeChat },
                { "群主", user => user.BelongsTo?.Name},
                { "余额", user => user.Balance.ToString("0.00") }
            };

        public XLWorkbook Export(IEnumerable<UserEntity> users)
        {
            var dt = new DataTable();
            foreach (var key in _columnMapper.Keys)
            {
                dt.Columns.Add(key);
            }

            foreach (var user in users)
            {
                var row = dt.NewRow();

                foreach (var key in _columnMapper.Keys)
                {
                    row[key] = _columnMapper[key](user);
                }
                dt.Rows.Add(row);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt, "User");
            return wb;
        }
    }
}
