using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Reddit()
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://www.reddit.com/r/Grimdank/.json");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();

            JObject redditJSON = JObject.Parse(data);
            List<JToken> posts = redditJSON["data"]["children"].ToList();
            
            List<Post> rp = new List<Post>();
 

            for (int i = 0; i < posts.Count ; i++)
            {
                Post rePost = new Post();

                rePost.Title = posts[i]["data"]["title"].ToString();
                rePost.ImageURL = posts[i]["data"]["thumbnail"].ToString();
                rePost.LinkURL = "https://reddit.com" + posts[i]["data"]["permalink"].ToString();
                rp.Add(rePost);
            }

            return View(rp);
        }
        
        

    }

}