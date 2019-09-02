using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Forum.Models.Data
{
    [Table("tblMessage")]
    public class MessageDTO
    {
        [Key]
        public int MessageId { get; set; }
        public int ArticleId { get; set; }
        public string Account { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }

        [ForeignKey("ArticleId")]
        public virtual ArticleDTO Article { get; set; }
        [ForeignKey("Account")]
        public virtual MemberDTO Member { get; set; }
    }
}