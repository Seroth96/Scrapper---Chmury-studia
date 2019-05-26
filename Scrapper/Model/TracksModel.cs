using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Scrapper.Model
{
    public class TracksModel
    {
        public List<Song> Tracks { get; set; } = new List<Song>();

        public void ParseAudioLibrary()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            foreach (var resourceName in resourceNames)
            {
                try
                {
                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        String line;
                        line = sr.ReadToEnd();

                        dynamic myObject = JToken.Parse(line);
                        // Log sessionA first order
                        var Trackss = new List<dynamic>();
                        var test = myObject["tracks"];
                        foreach (var track in myObject["tracks"])
                        {
                            Trackss.Add(track);
                        }
                        Tracks.AddRange(Trackss.Select(i => 
                            new Song
                            {
                                artist = i["artist"],
                                title = i["title"],
                                genre = i["genre"],
                                url = i["download_url"]
                            }).ToList());  
                    }
                }
                catch
                {
                    //skipping resource, which is not json with tracks
                }                
            }
            
        }
    }

}
