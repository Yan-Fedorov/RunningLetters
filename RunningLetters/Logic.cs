using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RunningLetters
{
    public interface ILogic
    {
        int Page { get; set; }
        List<RunningLetter> _runningLetters { get; set; }
    } 
    
    public class Logic: ILogic
    {
        
        private readonly RunningLetter _runningLetter;
        private readonly GameDataService _gameDataService;
        public Logic(RunningLetter runningLetter, GameDataService gameDataService)
        {
            _runningLetter = runningLetter;
            _gameDataService = gameDataService;
        }
        public Thread backgroundGame;
        public List<RunningLetter> _runningLetters { get; set; } = new List<RunningLetter>();

        public int Page { get; set; } = 1;
        private int pageCoef = 0;
        public bool FieldOver = false;
        public bool ExitGame = false;

        int LastLetter = 0;

        public bool ChangePage = false;
        public void Game()
        {
            
            while (!ExitGame)
            {
                var savedLetters = _gameDataService.LoadDatas();
                //Console.Clear();
                Console.SetCursorPosition(40, 20);
                Console.WriteLine("Page {0}", Page);
                //pageCoef = (Page - 1) * 900;
                for (int i = 0; i< savedLetters.Count; i++)
                {
                    if(savedLetters[i].LetterPage == Page)
                    _runningLetter.CreateLetter(_runningLetters, savedLetters[i].TargetPositionX, savedLetters[i].TargetPositionY, savedLetters[i].Symbol, Page);
                }

                while (!FieldOver && !ChangePage)
                {
                    FillField();
                }
                LastLetter = _runningLetters.Count();
                SaveLetters();
                Page++;
                
                Console.Clear();
                ChangePage = false;
                FieldOver = false;
                
            }       
        }
        public void SaveLetters()
        {
            _gameDataService.Save(_runningLetters);
        }
        
        private void FillField()
        {
            bool completeFilling = false;
            var field = new char[30, 30];
            for(int i = LastLetter; i< _runningLetters.Count; i++)
            {
                completeFilling = _runningLetters[i].Run(_runningLetters);
            }
            RenderTo(field, _runningLetters);
            DrowField(field);
            System.Threading.Thread.Sleep(5);
        }

        

        private void DrowField(char[,] field)
        {
            for (int y = 0; y < field.GetLength(1); y++)
            {
                Console.SetCursorPosition(0, y);
                for(int x = 0; x< field.GetLength(0); x++)
                {
                    Console.Write(field[x, y]);
                }
            }           
        }
        public void RenderTo(char[,] field, List<RunningLetter> _runningLetters)
        {
            for (int i = LastLetter; i < _runningLetters.Count; i++)
            {
                if (field.GetLength(0) > _runningLetters[i].StartPositionX && field.GetLength(1) > _runningLetters[i].StartPositionY)
                    field[_runningLetters[i].StartPositionX, _runningLetters[i].StartPositionY] = _runningLetters[i].Symbol;
            }
        }
    }
}
