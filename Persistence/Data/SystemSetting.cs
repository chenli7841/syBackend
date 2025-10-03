using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SystemSetting
    {
        public int Id { get; set; }
        public bool EnableProfileUpdate { get; set; }
        public string BatchConfirmMessage { get; set; }
        public string SchedulePickUpText { get; set; }
    }
}
