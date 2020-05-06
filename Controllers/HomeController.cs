using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokemonApp.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace PokemonApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<jsonData>));
            
            string jsonString;
            using (var reader = new System.IO.StreamReader(@"/Users/takahiro/Projects/PokemonApp/pokemon.json/pokedex.json"))
            {
                jsonString = reader.ReadToEnd();
            }
            var jsonList = JsonUtility.Deserialize<List<jsonData>>(jsonString);

            List<TableViewModel> viewModels = new List<TableViewModel>();
            foreach (jsonData json in jsonList)
            {
                viewModels.Add(new TableViewModel
                {
                    No = json.id,
                    //No = string.Format(@"<span onclick=""alert('{1} です。');"" >{0}</span>", json.name.english, json.name.japanese),
                    //Thumnail = string.Format(@"<a style=""background-image:url(""{0}"");"" href=""{1}"" />", "thumbnails/" + json.id.ToString("000") + json.name.english + ".png", "detail?no=" + json.id),
                    //Thumnail = string.Format(@"<img src=""{0}"" onclick=""location.href='{1}';"" />", "thumbnails/" + json.id.ToString("000") + json.name.english + ".png", "/Home/Sample"),
                    Thumnail = string.Format(@"<img src=""{0}"" onclick=""alert('{1} です。');"" />", "thumbnails/" + json.id.ToString("000") + json.name.english + ".png", json.name.japanese),
                    Name = string.Format(@"<span onclick=""alert('{1} です。');"" >{0}</span>", json.name.english, json.name.japanese),
                    //Name = json.name.english,
                    Type = string.Format(@"<span onclick=""alert('{1} です。');"" >{0}</span>", string.Join(",", json.type), json.name.japanese),
                    //Type = string.Join(",", json.type)
                });
            }
;
            return View(viewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class jsonData
    {
        public int id;
        public name name;
        public string[] type;    
    }

    public struct name
    {
        public string english;
        public string japanese;
        public string chinese;
    }

    public static class JsonUtility
    {
        /// <summary>
        /// Jsonメッセージをオブジェクトへデシリアライズします。
        /// </summary>
        public static T Deserialize<T>(string message)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(message)))
            {
                //var setting = new DataContractJsonSerializerSettings()
                //{
                //    UseSimpleDictionaryFormat = true,
                //};
                var serializer = new DataContractJsonSerializer(typeof(T)/*, setting*/);
                return (T)serializer.ReadObject(stream);
            }
        }
    }
}

