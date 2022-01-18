using System;

namespace Marche
{
    public static class Program
    {
        public static GameManager Game;
        [STAThread]
        static void Main()
        {
            using (var game = new GameManager())
            {
                Game = game;
                Game.Run();
            }
        }
    }
}
