using Domain.Entities;

namespace Common
{
    public interface ISystemSession
    {
        bool IsAuthenticated { get; set; }
        UserEntity CurrentUser { get; set; }
    }
}
