using Forum.Models.Data;
using Forum.Models.ViewModel.Article;
using Forum.Models.ViewModel.Message;
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
        public ActionResult Posts(int? page)
        {
            List<ArticleVM> article;
            List<ArticleListVM> articleList = new List<ArticleListVM>();

            var pageNum = page ?? 1;

            using(Db db = new Db()) {
                article = db.Articles.ToArray()
                    .Select(x => new ArticleVM(x)).ToList();

                foreach(var post in article) {

                    MemberDTO member = db.Members.Where(x => x.UID == post.UID).FirstOrDefault();

                    articleList.Add(new ArticleListVM()
                    {
                        ArticleId = post.ArticleId,
                        Title = post.Title,
                        Author = member.Account,
                        CreateTime = post.CreateTime,
                        Watch = post.Watch
                    });
                }
            }

            var onePageOfPost = articleList.ToPagedList(pageNum, 10);
            ViewBag.OnePageOfPost = onePageOfPost;

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

        // GET: Article/Post/id
        public ActionResult Post(int id)
        {
            ArticleVM model;

            using(Db db = new Db()) {
                ArticleDTO dto = db.Articles.Find(id);

                if(dto == null) {
                    return Content("Post isn't exist");
                }

                model = new ArticleVM(dto);
            }

            return View("Post", model);
        }

        

        // GET: Article/CommentList
        [Authorize]
        public ActionResult CommentList(int id)
        {
            List<MessageVM> messageList = new List<MessageVM>();
            using (Db db = new Db()) {
                messageList = db.Messages.ToArray().Select(x => new MessageVM(x)).Where(x => x.ArticleId == id).ToList();
            }
            return PartialView(messageList);
        }

        // POST: Article/AddComment
        [HttpPost]
        [Authorize]
        public ActionResult AddComment(int id, string Content)
        {
            string username = User.Identity.Name;

            using (Db db = new Db()) {
                MessageDTO dto = new MessageDTO();

                var q = db.Members.FirstOrDefault(x => x.Account == username);
                int userid = q.UID;

                dto.ArticleId = id;
                dto.UID = userid;
                dto.Content = Content;
                dto.CreateTime = DateTime.Now;

                db.Messages.Add(dto);
                db.SaveChanges();
            }

            return RedirectToAction("Post", new { id = id });
        }


    }
}