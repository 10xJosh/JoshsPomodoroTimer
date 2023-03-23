using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace JoshsPomodoroTimer.Functions
{
    internal class Alarm
    {
        //TODO: Add code that plays the alarm
        // - Create event for when timer is finished and this method can be called

        public Alarm()
        {

        }

        public static void PlayAlarm(string path)
        {
            try
            {
                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Volume = FrmSettings.Volume;
                mediaPlayer.Open(new Uri(path));
                mediaPlayer.Play();
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error playing the audio.","Error Playing Audio", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
