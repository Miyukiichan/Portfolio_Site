using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Services;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(JsonFileArticleService articleService)
        {
            this.ArticleService = articleService;
        }
        JsonFileArticleService ArticleService { get; }
        public IActionResult Index()
        {
            ViewData["TopArticles"] = ArticleService.TopArticles();
            return View();
        }
    }
}
