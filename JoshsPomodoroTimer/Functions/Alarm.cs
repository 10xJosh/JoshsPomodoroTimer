using System;
using System.Windows;
using System.Windows.Media;

namespace JoshsPomodoroTimer.Functions
{
    internal class Alarm
    {
        public Alarm() { }

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
                MessageBox.Show("There was an error playing the audio.","Error Playing Audio", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
