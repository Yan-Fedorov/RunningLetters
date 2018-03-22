using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLetters
{
    public interface IRunningLetter
    {
        void CreateLetter(List<RunningLetter> _runningLetters, int targetPosX, int targetPosY, char symbol, int page, int? positionInArray = null);
    }
    
    public class RunningLetter: IRunningLetter
    {

        public RunningLetter()
        {

        }
        public int TargetPositionX;
        public int TargetPositionY;

        public int StartPositionX;
        public int StartPositionY;

        public char Symbol;

        public int LetterPage;
        
        private int Switcher;
        public  void CreateLetter(List<RunningLetter> _runningLetters, int targetPosX, int targetPosY, char symbol, int page, int? positionInArray = null)
        {
            Random random = new Random(); 
            RunningLetter runningLetter = new RunningLetter();
            runningLetter.Symbol = symbol;
            runningLetter.TargetPositionX = targetPosX;
            runningLetter.TargetPositionY = targetPosY;
            runningLetter.LetterPage = page;
            Switcher = random.Next(0, 2);
            if (Switcher == 0)
            {
                var Y = random.Next(0, 2);
                runningLetter.StartPositionX = random.Next(0, 29);
                if(Y == 0)
                runningLetter.StartPositionY = 0;
                else runningLetter.StartPositionY = 29;
            }
            else if(Switcher == 1)
            {
                var X = random.Next(0, 2);
                if(X == 0)
                runningLetter.StartPositionX = 0;
                else runningLetter.StartPositionX = 29;
                runningLetter.StartPositionY = random.Next(0, 29);
            }
            if(positionInArray.HasValue)
            {
                _runningLetters.Insert(positionInArray.Value, runningLetter);
            }
            else 
            _runningLetters.Add(runningLetter);
        }
        public bool Run(List<RunningLetter> _runningLetters)
        {
            var numberOfLetters = _runningLetters.Count;
            if (TargetPositionX != StartPositionX)
            {
                if (TargetPositionX > StartPositionX)
                    StartPositionX++;
                else StartPositionX--;
            }
            else if (TargetPositionY != StartPositionY)
            {
                if (TargetPositionY > StartPositionY)
                    StartPositionY++;
                else StartPositionY--;
            }
            else numberOfLetters--;
            if (numberOfLetters == 0)
                return true;
            else return false;
        }
      

       

    }
}
