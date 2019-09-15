using Forum.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models.Features
{
    public class ArticleFeatures
    {
        public void Watch(int Id)
        {
            using (Db db = new Db()) {
                ArticleDTO dto = db.Articles.Find(Id);
                dto.Watch += 1;
                db.SaveChanges();
            }
        }

    }
}