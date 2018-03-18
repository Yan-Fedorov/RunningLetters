﻿using System.Collections.Generic;

namespace RunningLetters
{
    public class CheckCollision
    {
        private readonly ILogic _logic;
        private readonly IRunningLetter _runningLetter;
        public CheckCollision(ILogic logic, IRunningLetter runningLetter)
        {
            _logic = logic;
            _runningLetter = runningLetter;
        }
        public void CheckСollisionAtPosition(int targetX, int targetY, List<RunningLetter> _runningLetters, char symbol, int page)
        {
            if (_runningLetters.Count == 0)
            {
                _runningLetters.Add(new RunningLetter());
            }
            foreach (var letter in _runningLetters.ToArray())
            {
                if (letter.TargetPositionY == targetY && letter.Symbol == '@' && letter.LetterPage == page)
                {
                    if (targetY == 27)
                    {
                        _logic.Page++;
                    }
                    targetY++;
                }
                else if (letter.TargetPositionX == targetX && letter.TargetPositionY == targetY && letter.LetterPage == page)
                {
                    if (targetX < 27)
                    {
                        targetX++;
                    }
                    else if (targetX >= 27 && targetY < 27)
                    {
                        targetY++;
                        targetX = 2;
                    }
                    else if (targetY == 27)
                    {
                        targetY = 2;
                        _logic.Page++;
                    }
                }

            }
            _runningLetter.CreateLetter(_logic._runningLetters, targetX, targetY, symbol, page);


        }
    }
}
