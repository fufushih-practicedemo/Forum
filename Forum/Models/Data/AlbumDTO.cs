using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Forum.Models.Data
{
    [Table("tblAlbum")]
    public class AlbumDTO
    {
        [Key]
        public int AlbumId { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public int UID { get; set; } // tblAccount
        public DateTime CreateTime { get; set; }

        [ForeignKey("UID")]
        public virtual MemberDTO Member { get; set; }
    }
}