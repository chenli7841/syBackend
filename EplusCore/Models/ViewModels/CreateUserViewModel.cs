using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models.ViewModels
{
    public class CreateUserViewModel
    {
        public IEnumerable<RoleEntity> Roles { get; set; }
    }
}
