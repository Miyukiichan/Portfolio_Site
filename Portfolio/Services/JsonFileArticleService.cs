using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Portfolio.Models;

namespace Portfolio.Services
{
    public class JsonFileArticleService
    {
        public JsonFileArticleService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }
        private string Articles { get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "articles.json"); } }
        /*Get the full path of the file containing the article content*/
        private string ArticleFile(string file) => Path.Combine(WebHostEnvironment.WebRootPath, "data", "articles", file + ".html");
        /*Get the full path of the article image file*/
        public string ArticleImage(Article article) => Path.Combine("data", "images", article.Image);
        public Article Article(string id)
        {
            try
            {
                return GetArticles().First<Article>(a => a.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /*Get a list of JSON serialized articles from the articles.json file*/
        public IEnumerable<Article> GetArticles()
        {
            using var jsonFileReader = File.OpenText(Articles);
            return JsonSerializer.Deserialize<Article[]>(jsonFileReader.ReadToEnd(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        public IEnumerable<Article> TopArticles(int amount = 3)
        {
            return GetArticles().Take(amount);
        }
        /*Read the content file of a given article and store it as a HTML string to be rendered*/
        public HtmlString GetArticleContent(Article article)
        {
            try
            {
                using var file = File.OpenText(ArticleFile(article.ArticleFile));
                try
                {
                    return new HtmlString(file.ReadToEnd());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return new HtmlString("File \"" + article.ArticleFile + "\" could not be read");
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
                return new HtmlString("File \"" + article.ArticleFile + "\" not available");
            }
        }
    }
}
