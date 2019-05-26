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
using Scrapper.Model;

namespace Scrapper.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class ScrapperController : ControllerBase
    {
        [HttpGet("{number}")]
        public String Get(int number)
        {
            TracksModel model = new TracksModel();
            model.ParseAudioLibrary();
            string result = "";
            var item = new
            {
                Songs = model.Tracks.Take(number).ToList()
            };
            var jsonObject = JObject.FromObject(item);
            JArray array = (JArray)jsonObject["Songs"];
            result = array.ToString();

            return result;
        }

        [HttpGet]
        public String Get()
        {
            TracksModel model = new TracksModel();
            model.ParseAudioLibrary();
            string result = "";
            var item = new
            {
                Songs = model.Tracks
            };
            var jsonObject = JObject.FromObject(item);
            JArray array = (JArray)jsonObject["Songs"];
            result = array.ToString();

            return result;
        }

    }
}
