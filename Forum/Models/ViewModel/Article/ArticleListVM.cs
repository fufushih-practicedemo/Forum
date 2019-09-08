using Forum.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models.ViewModel.Article
{
    public class ArticleListVM
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public int Watch { get; set; }
    }
}