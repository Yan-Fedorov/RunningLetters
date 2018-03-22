using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using RunningLetters;
using Xunit;

namespace Unit
{
    public class CollisionTester
    {
        private ILogic _logic;
        private IRunningLetter _runningLetter;
        
        [Fact]
        public void PrintAndRenderFixture()
        {
            var letters = Load("12@2Ø3@4◄◄6…12");
            var page0 = Render(letters, 0);
            var page1 = Render(letters, 1);

            Assert.Equal(10, letters.Count);
            Assert.Equal(
@"12@
2 3@
4

6", page0);
            Assert.Equal("12", page1);
        }


        [Fact]
        public void ShouldStartFromNewLineAfterEnter()
        {
            var collision = Init("123@");
            
            collision.CheckСollisionAtPosition(2, 2, _logic._runningLetters, '4', 0);

            var result = Render(_logic._runningLetters, 0);
            Assert.Equal(
@"123@
4", result);
            Assert.Equal(5, _logic._runningLetters.Count);
        }

        [Fact]
        public void ShouldFoundEmtyPlace()
        {
            var tester = Init("1Ø23@4 5Ø6@7");

            tester.CheckСollisionAtPosition(2, 2, _logic._runningLetters, '8', 0);
            Assert.Equal(
@"1823@
4 5 6@
7", Render(_logic._runningLetters, 0));

            
            tester.CheckСollisionAtPosition(2, 2, _logic._runningLetters, '9', 0);
            Assert.Equal(
@"1823@
4 596@
7", Render(_logic._runningLetters, 0));

            
            tester.CheckСollisionAtPosition(2, 2, _logic._runningLetters, '0', 0);
            Assert.Equal(
@"1823@
4 596@
70", Render(_logic._runningLetters, 0));
        }

        
        private CheckCollision Init(string text)
        {
            _logic = Substitute.For<ILogic>();
            _logic._runningLetters = Load(text);
            
            _runningLetter = new RunningLetter();
            
            return new CheckCollision(_logic, _runningLetter);
        }
        
        
        public List<RunningLetter> Load(string text)
        {
            int
                page = 0,
                pointX = 1, // 
                pointY = 2;


            return text.Select(sym =>
            {
                pointX++;
                if (pointX > 26)
                {
                    IncY();
                    pointX = 2;
                }

                var l = new RunningLetter
                {
                    Symbol = sym,
                    TargetPositionX = pointX,
                    TargetPositionY = pointY,
                    LetterPage = page
                };

                if (sym == '@')
                {
                    pointX = 1; // 
                    IncY();
                }

                if (sym == 'Ø')
                    return null;

                if (sym == '◄')
                {
                    pointX = 1; // 
                    IncY();
                    return null;
                }

                if (sym == '…')
                {
                    pointX = 1; //
                    pointY = 2;
                    page++;
                    return null;
                }

                return l;

                void IncY()
                {
                    pointY++;
                    if (pointY <= 26) return;
                    
                    pointY = 2;
                    page++;
                }
            }).Where(x => x != null).ToList();
        }


        public string Render(List<RunningLetter> letters, int page)
        {
            var field = new char[27, 27];
            foreach (var letter in letters.Where(x => x.LetterPage == page))
            {
                field[letter.TargetPositionX-2, letter.TargetPositionY-2] = letter.Symbol;
            }

            var ret = "";
            var subTable = "";
            for (var y = 0; y < 27; y++)
            {
                var row = "";
                var subRow = "";
                
                for (var x = 0; x < 27; x++)
                {
                    var sym = field[x, y];

                    subRow += sym == 0 ? ' ' : sym;
                    if (sym != 0)
                    {
                        row += subRow;
                        subRow = "";
                    }
                }

                subTable += row + "\r\n";
                if (row.Length > 0)
                {
                    ret += subTable;
                    subTable = "";
                }

            }
            return ret.TrimEnd('\r', '\n');
        }
    }
}