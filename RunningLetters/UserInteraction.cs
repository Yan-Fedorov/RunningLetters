using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningLetters
{
    public class UserInteraction
    {
        private readonly RunningLetter _runningLetter;
        private readonly Logic _logic;
        private readonly CheckCollision _checkCollision;

        public UserInteraction(RunningLetter runningLetter, Logic logic, CheckCollision checkCollision)
        {
            _runningLetter = runningLetter;
            _logic = logic;
            _checkCollision = checkCollision;
        }

        public void LeftArrowEvent()
        {
            if(_logic.Page >1)
            _logic.Page-=2;
            _logic.ChangePage = true;
        }

        public void RightArrowEvent()
        {
            _logic.ChangePage = true;
        }
        
        public void GetSymbol()
        {
            while (true)
            {
                var page = _logic.Page;
                for (int y = 2; y < 28; y++)
                {
                    for (int x = 2; x < 28; x++)
                    {
                        var symbol = Console.ReadKey();

                        if (symbol.Key == ConsoleKey.Tab)
                        {
                            _logic.ExitGame = true;
                            _logic.SaveLetters();
                            return;
                        }
                        else if (symbol.Key == ConsoleKey.LeftArrow) LeftArrowEvent();
                        else if (symbol.Key == ConsoleKey.RightArrow) RightArrowEvent();
                        else if(page != _logic.Page)
                        {
                            y = 2;
                            x = 2;
                            page = _logic.Page;

                        }
                        else if (symbol.Key != ConsoleKey.Enter)
                        {

                            _checkCollision.CheckСollisionAtPosition(x, y, _logic._runningLetters, symbol.KeyChar, _logic.Page);
                        }
                        else if (symbol.Key == ConsoleKey.Enter)
                        {
                            _checkCollision.CheckСollisionAtPosition(x, y, _logic._runningLetters, '@', _logic.Page);
                            break;
                        }

                    }
                }
                _logic.FieldOver = true;
            }
        }
        
    }
}
//public void CheckСollisionAtPosition(int targetX, int targetY, List<RunningLetter> _runningLetters, char symbol, int page)
//{
//    if (RecursionCancel)
//    {
//        return;
//    }
//    if (ctr < MaxRecursiveCalls)
//    {
//        ctr++;
//        foreach (var letter in _runningLetters.ToArray())
//        {
//            if (letter.TargetPositionY == targetY && letter.Symbol == '@' && letter.LetterPage == page)
//            {
//                if (targetY == 27)
//                {
//                    targetY = 2;
//                    _logic.Page++;
//                    CheckСollisionAtPosition(targetX, targetY, _runningLetters, symbol, page);
//                }
//                targetY++;
//                CheckСollisionAtPosition(targetX, targetY, _runningLetters, symbol, page);
//            }
//            else if (letter.TargetPositionX == targetX && letter.TargetPositionY == targetY && letter.LetterPage == page)
//                if (targetX < 27)
//                {
//                    targetX++;
//                    CheckСollisionAtPosition(targetX, targetY, _runningLetters, symbol, page);
//                }
//                else if (targetX >= 27 && targetY < 27)
//                {
//                    targetY++;
//                    targetX = 2;
//                    CheckСollisionAtPosition(targetX, targetY, _runningLetters, symbol, page);
//                }
//                else if (targetY == 27)
//                {
//                    targetY = 2;
//                    _logic.Page++;
//                    CheckСollisionAtPosition(targetX, targetY, _runningLetters, symbol, page);
//                }
//        }
//    }
//    if (!RecursionCancel)
//    {
//        _runningLetter.CreateLetter(_logic._runningLetters, targetX, targetY, symbol, page);
//    }
//    RecursionCancel = true;
//}
