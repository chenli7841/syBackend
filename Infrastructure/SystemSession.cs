using Common;
using Domain.Entities;

namespace Infrastructure
{
    public class SystemSession : ISystemSession
    {
        public bool IsAuthenticated { get; set; }
        public UserEntity CurrentUser { get; set; }
    }
}