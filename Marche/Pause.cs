using System;
using System.Collections.Generic;
using System.Text;

namespace Marche
{
    public class Pause
    {
        public static bool _pauseState = false;

        public void IsPaused()
        {
            if(_pauseState == true)
            {
                _pauseState =! _pauseState;

                Console.WriteLine("Pause!");
            } else
            {
                _pauseState = !_pauseState;
                Console.WriteLine("UnPause!");
            }
        }

    }
}
