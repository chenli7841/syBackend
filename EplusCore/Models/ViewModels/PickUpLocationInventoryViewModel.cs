using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models.ViewModels
{
    public class PickUpLocationInventoryViewModel
    {
        public PickUpLocationInventoryViewModel()
        {
            Locations = new List<PickUpLocationEntity>();
            Areas = new List<PickUpLocationAreaEntity>();
            Agents = new List<UserEntity>();
        }

        public IEnumerable<PickUpLocationEntity> Locations { get; set; }
        public IEnumerable<PickUpLocationAreaEntity> Areas { get; set; }
        public IEnumerable<UserEntity> Agents { get; set; }
    }
}
