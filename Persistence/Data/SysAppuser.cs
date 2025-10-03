using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SysAppuser
    {
        public int Id { get; set; }
        public string UserNumber { get; set; }
        public bool UserType { get; set; }
        public string Role { get; set; }
        public int? Stage { get; set; }
        public bool? Enabled { get; set; }
        public string Phone { get; set; }
        public string NickName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Avatar { get; set; }
        public DateTime? LastPasswordResetTime { get; set; }
        public string PayPassword { get; set; }
        public long? AreaId { get; set; }
        public byte? IsCertification { get; set; }
        public string RealName { get; set; }
        public string IdCard { get; set; }
        public string IdCardFront { get; set; }
        public string IdCardBack { get; set; }
        public DateTime CreateTime { get; set; }
        public string QrPath { get; set; }
    }
}
