using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshsPomodoroTimer.Functions
{
    public class Settings
    {
        public double Volume { get; set; }
        public bool doesPauseEffectBreakEnabled { get; set; }
        public bool IsAutoStartBreakEnabled { get; set; }
        public string AlarmSound { get; set; }
        public int BreakDuration { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public Settings() 
        { 
            
        }

        public Settings(double volume, bool doesPauseEffectedBreakEnabled, bool IsAutoStartBreakEnabled,
            string alarmSound, int BreakDuration, int minutes, int seconds) 
        {
            this.Volume = volume;
            this.BreakDuration = BreakDuration;
            this.doesPauseEffectBreakEnabled = doesPauseEffectedBreakEnabled;
            this.AlarmSound = alarmSound;
            this.BreakDuration = BreakDuration;
            this.Minutes = minutes;
            this.Seconds = seconds;
        }

        public void SaveSettings(Settings settings)
        {
            this.Volume = settings.Volume;
            this.BreakDuration = settings.BreakDuration;
            this.doesPauseEffectBreakEnabled = settings.doesPauseEffectBreakEnabled;
            this.AlarmSound = settings.AlarmSound;
            this.BreakDuration = settings.BreakDuration;
        }

        public Settings LoadSettings()
        {
            Settings settings = new Settings();
            settings.Volume = this.Volume;
            settings.AlarmSound = this.AlarmSound;
            settings.BreakDuration = this.BreakDuration;
            settings.IsAutoStartBreakEnabled = this.IsAutoStartBreakEnabled;
            settings.doesPauseEffectBreakEnabled = this.doesPauseEffectBreakEnabled;

            return settings;
        }
    }
}
