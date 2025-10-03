using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BaseAlbum
    {
        public long Id { get; set; }
        public string AlblumInfo { get; set; }
        public ulong? AlbumDefault { get; set; }
        public string AlbumName { get; set; }
        public int? AlbumSequence { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? AlbumType { get; set; }
        public long? AlbumStoreId { get; set; }
        public string PictureKeys { get; set; }
    }
}
