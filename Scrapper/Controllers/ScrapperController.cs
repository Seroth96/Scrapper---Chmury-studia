using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Scrapper.Controllers
{
    public struct Song
    {
        public string artist { get; set; }
        public string title { get; set; }
        public string genre { get; set; }
        public string url { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ScrapperController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public String Get()
        {          
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Scrapper.Resources.audioswap_ajax.json";
            string result = "";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader sr = new StreamReader(stream))
            {
                String line;
                line = sr.ReadToEnd();

                dynamic myObject = JToken.Parse(line);
                // Log sessionA first order
                var Tracks = new List<dynamic>();
                var test = myObject["tracks"];
                foreach (var track in myObject["tracks"])
                {
                    Tracks.Add(track);
                }
                var item = new
                {
                    Songs = Tracks.Select(i => new Song
                    {
                        artist = i["artist"],
                        title = i["title"],
                        genre = i["genre"],
                        url = i["download_url"]
                    }).ToList()
                };

                var jsonObject = JObject.FromObject(item);
                result = jsonObject.ToString();
            }

            //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = result              
            //};
            return result;
        }


        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
