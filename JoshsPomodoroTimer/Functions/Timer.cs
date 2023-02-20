using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JoshsPomodoroTimer.Functions
{
    internal class Timer
    {
        public delegate int OnTimeChanged();
        public static event OnTimeChanged TimeChanged;

        public int Minutes { get; private set; } = 25;
        public int Seconds { get; private set; } = 0;

        public Timer()
        {

        }

        public (int Minutes, int Seconds) CountDown(int minutes, int seconds)
        {

            if (Seconds >= 60)
            {
                throw new ArgumentOutOfRangeException("Seconds cant go past 59. Please convert into minutes.");
            }

            if (Minutes != 0 && Seconds == 0)
            {
                Minutes--;
                Seconds = 59;
            }

            if (Seconds == 0 && Minutes == 0)
            {
                Seconds = 0;
            }
            else
            {
                Seconds--;
            }

            return (Minutes, Seconds);
        }
    }

}
