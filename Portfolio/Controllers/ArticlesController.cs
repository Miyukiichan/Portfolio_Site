using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Controllers
{
    [Route("[controller]",
        Name = "Articles")]
    public class ArticlesController : Controller
    {
        public ArticlesController(JsonFileArticleService articleService)
        {
            this.ArticleService = articleService;
        }
        /*Get the article of the given id and display the content*/
        [HttpGet("{id}")]
        public IActionResult ArticleContent(string id)
        {
            Article article = ArticleService.Article(id);
            if (article != null)
            {
                ViewData["Title"] = article.Title;
                ViewData["Description"] = article.Description;
                ViewData["Content"] = ArticleService.GetArticleContent(article);
            }
            else
            {
                ViewData["Content"] = "No article found";
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonFileArticleService ArticleService { get; }
    }
}
