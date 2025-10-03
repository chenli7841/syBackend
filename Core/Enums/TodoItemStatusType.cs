using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum TodoItemStatusType
    {
        [Description("已创建")]
        Created = 10,
        [Description("待处理")]
        PendingProcess = 20,
        [Description("已处理")]
        Processed = 30,
        [Description("已完成")]
        Completed = 40,
    }
}
