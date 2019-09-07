using Forum.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models.ViewModel.Article
{
    public class ArticleVM
    {
        public ArticleVM()
        {
            
        }

        public ArticleVM(ArticleDTO row)
        {
            ArticleId = row.ArticleId;
            Title = row.Title;
            Content = row.Content;
            UID = row.UID;
            CreateTime = row.CreateTime;
            Watch = row.Watch;
        }

        public int ArticleId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        public int UID { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public int Watch { get; set; }

    }
}