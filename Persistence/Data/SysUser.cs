using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SysUser
    {
        public long UserId { get; set; }
        public long? ShopId { get; set; }
        public string Username { get; set; }
        public string NickName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Password { get; set; }
        public ulong? IsAdmin { get; set; }
        public long? Enabled { get; set; }
        public ulong? IsDel { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? PwdResetTime { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
