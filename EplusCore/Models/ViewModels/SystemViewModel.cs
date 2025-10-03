using System.Collections.Generic;
using Domain.Entities;

namespace WebUI.Models.ViewModels
{
    public class SystemViewModel
    {
        public SystemViewModel()
        {
            Photos = new List<SystemPhotoEntity>();
        }

        public SystemSettingsEntity Settings { get; set; }

        public IList<SystemPhotoEntity> Photos { get; set; }
    }
}
