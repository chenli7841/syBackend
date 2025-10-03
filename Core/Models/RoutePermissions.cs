using System.Collections.Generic;

namespace Domain.Models
{
    public class RoutePermissions
    {
        public RoutePermissions()
        {
            UserPermissions = new List<RouteUserPermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public IList<RouteUserPermission> UserPermissions { get; set; }
    }
}
