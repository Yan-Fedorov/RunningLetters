using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLetters
{
    public class GameDataService
    {
        private readonly string _path;

        public GameDataService()
        {
            var localPath = Path.GetDirectoryName(GetType().Assembly.Location);
            _path = Path.Combine(localPath, @"GameData.json");
        }

        public List<RunningLetter> LoadDatas()
        {
            if (!File.Exists(_path))
                return new List<RunningLetter>();

            using (var file = File.OpenText(_path))
            {
                var data = file.ReadToEnd();
                var jObject = JArray.Parse(data);
                return jObject.ToObject<List<RunningLetter>>().ToList();
            }
        }

        public void Save(List<RunningLetter> gameData)
        {
            RunningLetter newLetter = new RunningLetter();
            var games = LoadDatas();
            foreach (var letter in gameData)
            {
                foreach (var priviosLetter in games) {
                    if (letter == priviosLetter)
                        break;
                    else if(newLetter != letter)
                        newLetter = letter;
                }
                games.Add(newLetter);
            }

            using (var file = File.Open(_path, FileMode.Create))
            using (var writer = new StreamWriter(file))
            {
                writer.Write(JsonConvert.SerializeObject(games));
                writer.Flush();
            }
        }
    }
}
