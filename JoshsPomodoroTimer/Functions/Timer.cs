using System;

namespace JoshsPomodoroTimer.Functions
{
    internal class Timer
    {
        public int Minutes { get; set; } = 25;
        public int Seconds { get; set; } = 0;

        public Timer() { }

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
                return (Minutes, Seconds);
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
