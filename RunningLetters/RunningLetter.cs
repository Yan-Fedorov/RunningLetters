using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLetters
{
    public interface IRunningLetter
    {
        void CreateLetter(List<RunningLetter> _runningLetters, int targetPosX, int targetPosY, char symbol, int page);
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
        public  void CreateLetter(List<RunningLetter> _runningLetters, int targetPosX, int targetPosY, char symbol, int page)
        {
            Random random = new Random(); 
            RunningLetter _runningLetter = new RunningLetter();
            _runningLetter.Symbol = symbol;
            _runningLetter.TargetPositionX = targetPosX;
            _runningLetter.TargetPositionY = targetPosY;
            _runningLetter.LetterPage = page;
            Switcher = random.Next(0, 2);
            if (Switcher == 0)
            {
                var Y = random.Next(0, 2);
                _runningLetter.StartPositionX = random.Next(0, 29);
                if(Y == 0)
                _runningLetter.StartPositionY = 0;
                else _runningLetter.StartPositionY = 29;
            }
            else if(Switcher == 1)
            {
                var X = random.Next(0, 2);
                if(X == 0)
                _runningLetter.StartPositionX = 0;
                else _runningLetter.StartPositionX = 29;
                _runningLetter.StartPositionY = random.Next(0, 29);
            }
            _runningLetters.Add(_runningLetter);
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
