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
            bool intersection = false;
            var savedLetters = LoadDatas();
            foreach (var currentLetter in gameData)
            {
                foreach (var savedLetter in savedLetters) {
                    if (CheckOverlay(currentLetter, savedLetter))
                    {
                        intersection = true; 
                        break;
                    }
                }
                if(!intersection)
                    savedLetters.Add(currentLetter);
                intersection = false;
            }
            using (var file = File.Open(_path, FileMode.Create))
            using (var writer = new StreamWriter(file))
            {
                writer.Write(JsonConvert.SerializeObject(savedLetters));
                writer.Flush();
            }
        }

        private bool CheckOverlay(RunningLetter currentLetter, RunningLetter savedLetter)
        {
            if (currentLetter.TargetPositionX != savedLetter.TargetPositionX || currentLetter.TargetPositionY != savedLetter.TargetPositionY || currentLetter.LetterPage != savedLetter.LetterPage)
                return false;
                return true;
        }
    }
}
