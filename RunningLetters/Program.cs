using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RunningLetters
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            using (var container = IoCBuilder.Building())
            {
                using (var scope = container.BeginLifetimeScope())
                {

                    var logica = scope.TryResolve<Logic>(out var logic);
                    var userInter = scope.TryResolve<UserInteraction>(out var user);
                    

                    logic.backgroundGame = new Thread(logic.Game);
                    logic.backgroundGame.Start();
                    logic.backgroundGame.IsBackground = true;

                    user.GetSymbol();
                    
                }
            }
        }
    }
}
