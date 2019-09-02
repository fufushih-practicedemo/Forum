using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Forum.Models.Data
{
    [Table("tblArticle")]
    public class ArticleDTO
    {
        [Key]
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Account { get; set; }
        public DateTime CreateTime { get; set; }
        public int Watch { get; set; }

        [ForeignKey("Account")]
        public virtual MemberDTO Member { get; set; }
    }
}