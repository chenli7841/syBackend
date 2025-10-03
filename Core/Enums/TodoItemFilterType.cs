using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum TodoItemFilterType
    {
        None = 1,
        [Description("我创建的待办事项")]
        Created = 2,
        [Description("我经办的待办事项")]
        Assigned = 3,
    }
}
