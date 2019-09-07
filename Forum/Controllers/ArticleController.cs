using Forum.Models.Data;
using Forum.Models.ViewModel.Article;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article/Posts
        public ActionResult Posts(int? page, int? articleId)
        {
            List<ArticleVM> articleList;

            var pageNum = page ?? 1;

            using(Db db = new Db()) {
                articleList = db.Articles.ToArray().Where(x => articleId == null || articleId == 0 || x.ArticleId == articleId)
                    .Select(x => new ArticleVM(x)).ToList();
            }

            var onePageOfDefault = articleList.ToPagedList(pageNum, 10);


            return View(articleList);
        }

        // GET: Article/AddPosts
        [Authorize]
        public ActionResult AddPosts()
        {
            return View();
        }

        // POST: Article/AddPosts
        [Authorize]
        [HttpPost]
        public ActionResult AddPosts(ArticleVM model)
        {
            // Get username
            string username = User.Identity.Name;

            if (!ModelState.IsValid) {
                return View(model);
            } else {
                using(Db db = new Db()) {
                    ArticleDTO dto = new ArticleDTO();

                    // Get user id
                    var q = db.Members.FirstOrDefault(x => x.Account == username);
                    int userid = q.UID;

                    dto.Title = model.Title;
                    dto.Content = model.Content;
                    dto.UID = userid;
                    dto.CreateTime = DateTime.Now;
                    dto.Watch = 0;

                    db.Articles.Add(dto);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Posts");
        }

        // GET: Article/EditPost
        [Authorize]
        public ActionResult EditPost(int id)
        {
            ArticleVM model;

            using(Db db = new Db()) {
                ArticleDTO dto = db.Articles.Find(id);

                if(dto == null) {
                    return Content("The Page isn't exitst");
                }

                model = new ArticleVM(dto);
            }

            return View(model);
        }

        // POST: Article/EditPost
        [Authorize]
        [HttpPost]
        public ActionResult EditPost(ArticleVM model)
        {
            if (!ModelState.IsValid) {
                return View(model);
            }

            using(Db db = new Db()) {
                int Id = model.ArticleId;

                ArticleDTO dto = db.Articles.Find(Id);
                dto.Title = model.Title;
                dto.Content = model.Content;

                db.SaveChanges();
            }

            return RedirectToAction("Posts");
        }

        // GET: Article/DeletePost
        [Authorize]
        public ActionResult DeletePost(int id)
        {
            using(Db db = new Db()) {
                ArticleDTO dto = db.Articles.Find(id);

                db.Articles.Remove(dto);
                db.SaveChanges();
            }

            return RedirectToAction("Posts");
        }
    }
}