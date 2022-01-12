using System;

namespace Marche
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
          //  using (var game = new Game1())
            //    game.Run();
            using (var game = new paysage())
                game.Run();
        }
    }
}
